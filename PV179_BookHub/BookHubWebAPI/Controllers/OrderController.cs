using AutoMapper;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.Facades.Order;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderFacade _orderFacade;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderController(IOrderFacade orderFacade, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orderFacade = orderFacade;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("{userId}")]
    public async Task<IActionResult> CreateOrder(long userId)
    {
        var order = await _orderFacade.CreateOrderAsync(userId);

        return Created(
            new Uri($"{Request.Path}/{order.Id}", UriKind.Relative), 
            order
            );
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> FetchOrdersByUserId(long userId)
    {
        return Ok(await _orderFacade.FetchOrdersByUserIdAsync(userId));
    }

    [HttpGet]
    [Route("detail/{id}")]
    public async Task<IActionResult> FetchOrderById(long id)
    {
        return Ok(await _orderFacade.FindOrderByIdAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        await _orderFacade.DeleteOrderByIdAsync(id);

        return NoContent();
    }

    [HttpPatch]
    [Route("pay/{id}")]
    public async Task<IActionResult> PayForOrder(long id)
    {
        return Ok(await _orderFacade.PayForOrderAsync(id));
    }

    [HttpPatch]
    [Route("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(long id)
    {
        return Ok(await _orderFacade.CancelOrderAsync(id));
    }

    [HttpPatch]
    [Route("refund/{id}")]
    public async Task<IActionResult> RefundOrder(long id)
    {
        return Ok(await _orderFacade.RefundOrderAsync(id));
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
