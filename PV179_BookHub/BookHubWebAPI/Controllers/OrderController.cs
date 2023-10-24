using AutoMapper;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;
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
    [Route("/{userId}")]
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

    [HttpDelete]
    [Route("/{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        var order = _unitOfWork.OrderRepository.GetById(id);
        if (order != null)
        {
            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.CommitAsync();
        }

        return NoContent();
    }

    [HttpPatch]
    [Route("/pay/{id}")]
    public async Task<IActionResult> PayForOrder(long id)
    {
        var order = _unitOfWork.OrderRepository.GetById(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Created)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Paid;
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }

    [HttpPatch]
    [Route("/cancel/{id}")]
    public async Task<IActionResult> CancellOrder(long id)
    {
        var order = _unitOfWork.OrderRepository.GetById(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Created)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Cancelled;
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }

    [HttpPatch]
    [Route("/refund/{id}")]
    public async Task<IActionResult> RefundOrder(long id)
    {
        var order = _unitOfWork.OrderRepository.GetById(id);
        if (order != null && order.State == DataAccessLayer.Models.Enums.OrderState.Paid)
        {
            order.State = DataAccessLayer.Models.Enums.OrderState.Refunded;
            await _unitOfWork.CommitAsync();
        }

        return Ok(_mapper.Map<DetailedOrderViewDto>(order));
    }
}
