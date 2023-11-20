using AutoMapper;
using BookHubWebAPI.Api.Order.Create;
using BookHubWebAPI.Api.Order.View;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("{userId}")]
    public async Task<IActionResult> CreateOrder(long userId)
    {
        var order = new Order()
        { 
            UserId = userId 
        };

        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{order.Id}", UriKind.Relative), 
            _mapper.Map<DetailedOrderViewDto>(order)
            );
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> FetchOrdersByUserId(long userId)
    {
        var orders = await _unitOfWork.OrderRepository
            .GetAllAsync(ord => ord.UserId == userId);

        return Ok(_mapper.Map<List<GeneralOrderViewDto>>(orders));
    }

    [HttpGet]
    [Route("detail/{id}")]
    public async Task<IActionResult> FetchOrderById(long id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order != null)
        {
            return Ok(_mapper.Map<DetailedOrderViewDto>(order));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order != null)
        {
            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.CommitAsync();
        }

        return NoContent();
    }

    [HttpPatch]
    [Route("pay/{id}")]
    public async Task<IActionResult> PayForOrder(long id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Created)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Paid;
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }

    [HttpPatch]
    [Route("cancel/{id}")]
    public async Task<IActionResult> CancellOrder(long id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Created)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Cancelled;
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }

    [HttpPatch]
    [Route("refund/{id}")]
    public async Task<IActionResult> RefundOrder(long id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Paid)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Refunded;
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }

    [HttpPost]
    [Route("item/{orderId}")]
    public async Task<IActionResult> CreateOrderItem(long orderId, CreateOrderItemDto createOrderItemDto)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }

        var inventoryItems = await _unitOfWork.InventoryItemRepository
            .GetAllAsync(
            item => 
            item.BookStoreId == createOrderItemDto.BookStoreId
            && item.BookId == createOrderItemDto.BookId
            );

        if (inventoryItems == null)
        {
            return NotFound();
        }

        if (inventoryItems.Count() != 1)
        {
            // return error
        }

        var singleItem = inventoryItems.FirstOrDefault();

        if (singleItem.InStock < createOrderItemDto.Quantity)
        {
            // throw error, not enough stock
        }

        singleItem.InStock -= createOrderItemDto.Quantity;


        var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);
        orderItem.OrderId = orderId;

        order.TotalPrice += orderItem.Price * orderItem.Quantity;

        _unitOfWork.OrderRepository.Update(order);
        _unitOfWork.InventoryItemRepository.Update(singleItem);
        await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/item/{orderItem.Id}", UriKind.Relative),
            _mapper.Map<DetailedOrderItemViewDto>(orderItem)
            );
    }

    [HttpGet]
    [Route("item/{id}")]
    public async Task<IActionResult> FetchSingleItem(long id)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
        if ( orderItem != null )
        {
            return Ok(_mapper.Map<DetailedOrderItemViewDto>(orderItem));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("item/{id}")]
    public async Task<IActionResult> DeleteOrderItem(long id)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }

        var inventoryItems = await _unitOfWork.InventoryItemRepository
            .GetAllAsync(
            item =>
            item.BookStoreId == orderItem.BookStoreId
            && item.BookId == orderItem.BookId
            );

        if (inventoryItems == null)
        {
            return NotFound();
        }

        if (inventoryItems.Count() != 1)
        {
            // return error
        }

        var singleItem = inventoryItems.FirstOrDefault();

        singleItem.InStock += orderItem.Quantity;

        _unitOfWork.InventoryItemRepository.Update(singleItem);
        _unitOfWork.OrderItemRepository.Delete(orderItem);
        await _unitOfWork.CommitAsync();

        return NoContent();
    }
}
