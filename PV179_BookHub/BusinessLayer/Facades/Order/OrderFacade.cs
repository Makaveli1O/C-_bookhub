using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.InventoryItem;
using BusinessLayer.Services.Order;

using DataAccessLayer.Models.Enums;

namespace BusinessLayer.Facades.Order;

public class OrderFacade : BaseFacade, IOrderFacade
{
    private readonly IOrderService _orderService;
    private readonly IGenericService<OrderItemEntity, long> _orderItemService;
    private readonly IGenericService<UserEntity, long> _userService;
    private readonly IGenericService<BookEntity, long> _bookService;
    private readonly IInventoryItemService _inventoryItemService;

    public OrderFacade(IMapper mapper, 
        IOrderService orderService, 
        IGenericService<OrderItemEntity, long> orderItemService,
        IGenericService<UserEntity, long> userService,
        IGenericService<BookEntity, long> bookService,
        IInventoryItemService inventoryItemService
        ) : base(mapper)
    {
        _orderService = orderService;
        _orderItemService = orderItemService;
        _userService = userService;
        _bookService = bookService;
        _inventoryItemService = inventoryItemService;
    }

    public async Task<DetailedOrderViewDto> CreateOrderAsync(long userId)
    {
        var order = new OrderEntity()
        {
            UserId = userId,
            User = await _userService.FindByIdAsync(userId),
            State = OrderState.Created
        };

        order = await _orderService.CreateAsync(order);

        return _mapper.Map<DetailedOrderViewDto>(order);
    }

    public async Task DeleteOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);

        if (order.State == OrderState.Created)
        {
            await ReturnStock(order);
        }

        await _orderService.DeleteAsync(order);
    }

    public async Task<IEnumerable<GeneralOrderViewDto>> FetchOrdersByUserIdAsync(long userId)
    {
        _ = await _userService.FindByIdAsync(userId);
        var orders = await _orderService.FetchAllByUserIdAsync(userId);
        var ordersDto = new List<GeneralOrderViewDto>();

        foreach (var order in orders)
        {
            var orderDto = _mapper.Map<DetailedOrderViewDto>(order);
            orderDto.TotalPrice = CalculateTotalPrice(order.Items);
            ordersDto.Add(orderDto);
        }

        return ordersDto;
    }

    private double CalculateTotalPrice(IEnumerable<OrderItemEntity>? items)
    {
        if (items == null)
        {
            return 0;
        }

        double total = 0;
        foreach (var item in items)
        {
            total += (item.Price * item.Quantity);
        }

        return total;
    } 

    public async Task<DetailedOrderViewDto> FindOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);
        var orderDto = _mapper.Map<DetailedOrderViewDto>(order);
        orderDto.TotalPrice = CalculateTotalPrice(order.Items);

        return orderDto;
    }

    private async Task<OrderEntity> UpdateOrder(long id, OrderState requiredState, OrderState newState, bool returnStock = true)
    {
        var order = await _orderService.FindByIdAsync(id);

        if (order.State != requiredState)
        {
            throw new WrongOrderStateException(order.State, newState);
        }

        if (returnStock)
        {
            await ReturnStock(order);
        }
        order.State = newState;
        await _orderService.UpdateAsync(order);

        return order;
    }

    public async Task<DetailedOrderViewDto> PayForOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            OrderState.Created, 
            OrderState.Paid,
            false));
    }

    public async Task<DetailedOrderViewDto> RefundOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            OrderState.Paid,
            OrderState.Refunded));
    }

    public async Task<DetailedOrderViewDto> CancelOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            OrderState.Created,
            OrderState.Cancelled));
    }

    private async Task ReturnStock(OrderEntity order)
    {
        if (order.Items != null)
        {
            foreach (var item in order.Items)
            {
                await AddBookStock(item.BookId, item.BookStoreId, item.Quantity);
            }
        }
    }

    // NOTE: use false for save, since we want to make it in single transaction
    private async Task LowerBookStock(long bookId, long bookStoreId, uint quantity)
    {
        await _inventoryItemService.ChangeStockAsync(
            bookId, bookStoreId, quantity, 
            StockDirection.StockReduction, false);  
    }

    private async Task AddBookStock(long bookId, long bookStoreId, uint quantity)
    {
        await _inventoryItemService.ChangeStockAsync(
            bookId, bookStoreId, quantity, 
            StockDirection.StockAddition, false);
    }

    public async Task<DetailedOrderItemViewDto> CreateOrderItem(long orderId, CreateOrderItemDto createOrderItemDto)
    {
        var order = await _orderService.FindByIdAsync(orderId);
        var book = await _bookService.FindByIdAsync(createOrderItemDto.BookId);

        if (order.State != OrderState.Created)
        {
            throw new WrongOrderStateException(orderId, order.State, "created");
        }

        await LowerBookStock(createOrderItemDto.BookId, createOrderItemDto.BookStoreId, createOrderItemDto.Quantity);

        var orderItem = _mapper.Map<OrderItemEntity>(createOrderItemDto);
        orderItem.Price = book.Price;
        orderItem.Order = order;
        orderItem.Book = book;
        orderItem = await _orderItemService.CreateAsync(orderItem);

        return _mapper.Map<DetailedOrderItemViewDto>(orderItem);
    }

    public async Task<DetailedOrderItemViewDto> FindOrderItemByIdAsync(long id)
    {
        var orderItem = await _orderItemService.FindByIdAsync(id);
        
        var orderItemDto = _mapper.Map<DetailedOrderItemViewDto>(orderItem);
        orderItemDto.Book = _mapper.Map<GeneralBookViewDto>(await _bookService.FindByIdAsync(orderItem.BookId));
        //var bookStore = await _bookStoreService.FindByIdAsync(orderItem.BookStoreId);

        return orderItemDto;
    }

    public async Task DeleteOrderItemByIdAsync(long id)
    {
        var orderItem = await _orderItemService.FindByIdAsync(id);
        await AddBookStock(orderItem.BookId, orderItem.BookStoreId, orderItem.Quantity);

        await _orderItemService.DeleteAsync(orderItem);
    }
}
