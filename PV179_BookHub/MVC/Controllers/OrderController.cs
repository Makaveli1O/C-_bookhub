using BusinessLayer.Facades.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Mapster;
using MVC.Models.Order;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.DTOs.Order.View;

namespace MVC.Controllers;

[Route("Orders")]
public class OrderController : Controller
{
    private readonly IOrderFacade _orderFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public OrderController(IOrderFacade orderFacade, UserManager<LocalIdentityUser> userManager)
    {
        _orderFacade = orderFacade;
        _userManager = userManager;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    [Route("{id:long}/Detail")]
    [HttpGet]
    public async Task<JsonResult> OrderDetail(long id)
    {
        //would be nicer if we had DetailedOrderItem here instead of the General one
        var order = await _orderFacade.FindOrderByIdAsync(id);

        var model = order.Adapt<OrderDetailViewModel>();

        return Json(model, _jsonSerializerOptions);
    }

    [HttpGet("User/{id:long}")]
    [Authorize]
    public async Task<JsonResult> SingleUserOrders(long id)
    {
        return Json(await _orderFacade.FetchOrdersByUserIdAsync(id));
    }

    [HttpGet]
    [Route("Create/{userId:long}")]
    public async Task<IActionResult> Create(long userId)
    {
		var order = await _orderFacade.CreateOrderAsync(userId);

        var orderDetail = order.Adapt<OrderDetailViewModel>();

		return View("OrderDetail", orderDetail);
	}

	[HttpGet]
    [Route("Delete/{orderId:long}")]
    public async Task<IActionResult> Delete(long orderId)
    {
        var user = await _userManager.GetUserAsync(User);

        var order = await _orderFacade.FindOrderByIdAsync(orderId);

        if (user == null || order.UserId != user.UserId)
        {
            return Unauthorized();
        }
        await _orderFacade.DeleteOrderByIdAsync(orderId);

		return NoContent();
    }

	[Authorize]
    [Route("{id:long}/Cancel")]
    public async Task<IActionResult> Cancel(long id)
    {
        var order = await _orderFacade.FindOrderByIdAsync(id);
        
        if (order.State != DataAccessLayer.Models.Enums.OrderState.Paid || order.State != DataAccessLayer.Models.Enums.OrderState.Created)
        {
            BadRequest();
        }

        var user = await _userManager.GetUserAsync(User);
        if(order.UserId != user.UserId)
        {
            Unauthorized();
        }

        var cancelStatus = await _orderFacade.CancelOrderAsync(id);

        return View("OrderCancel", cancelStatus);
    }
}
