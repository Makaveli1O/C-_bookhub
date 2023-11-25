using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.Order;
using OrderEntity = DataAccessLayer.Models.Purchasing.Order;
using OrderItemEntity = DataAccessLayer.Models.Purchasing.OrderItem;
using InventoryItemEntity = DataAccessLayer.Models.Logistics.InventoryItem;
using DataAccessLayer.Models.Enums;

namespace BusinessLayer.Facades.Order;

public class OrderFacade : BaseFacade, IOrderFacade
{
    private readonly IOrderService _orderService;
    private readonly IGenericService<OrderItemEntity, long> _orderItemService;
    private readonly IBookService _bookService;
    private readonly IGenericService<InventoryItemEntity, long> _inventoryItemService;

    public OrderFacade(IMapper mapper, 
        IOrderService orderService, 
        IGenericService<OrderItemEntity, long> orderItemService,
        IBookService bookService,
        IGenericService<InventoryItemEntity, long> inventoryItemService
        ) : base(mapper)
    {
        _orderService = orderService;
        _orderItemService = orderItemService;
        _bookService = bookService;
        _inventoryItemService = inventoryItemService;
    }

    public async Task<DetailedOrderViewDto> CreateOrderAsync(long userId)
    {
        var order = new OrderEntity()
        {
            UserId = userId,
            // TODO User = await _userService.FindByIdAsync(userId);
            State = DataAccessLayer.Models.Enums.OrderState.Created,
            TotalPrice = 0
        };

        await _orderService.CreateAsync(order);

        return _mapper.Map<DetailedOrderViewDto>(order);
    }

    public async Task DeleteOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);

        if (order.State == OrderState.Created)
        {
            // TODO return stock if possible
        }

        await _orderService.DeleteAsync(order);
    }

    public async Task<IEnumerable<GeneralOrderViewDto>> FetchOrdersByUserIdAsync(long userId)
    {
        //TODO await _userService.FindByIdAsync(userId);
        var orders = await _orderService.FetchAllByUserIdAsync(userId);

        return _mapper.Map<List<GeneralOrderViewDto>>(orders);
    }

    public async Task<DetailedOrderViewDto> FindOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);

        return _mapper.Map<DetailedOrderViewDto>(order);
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
            // TODO
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

    private async Task LowerBookStock(long bookId, long bookStoreId, uint quantity)
    {

    }

    private async Task AddBookStock(long bookId, long bookStoreId, uint quantity)
    {

    }

    public async Task<DetailedOrderItemViewDto> CreateOrderItem(long orderId, CreateOrderItemDto createOrderItemDto)
    {
        var order = await _orderService.FindByIdAsync(orderId);

        if (order.State != OrderState.Created)
        {
            throw new WrongOrderStateException(orderId, order.State, "created");
        }

        await LowerBookStock(createOrderItemDto.BookId, createOrderItemDto.BookStoreId, createOrderItemDto.Quantity);

        var orderItem = _mapper.Map<OrderItemEntity>(createOrderItemDto);
        orderItem.Order = order;
        await _orderItemService.CreateAsync(orderItem);

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
