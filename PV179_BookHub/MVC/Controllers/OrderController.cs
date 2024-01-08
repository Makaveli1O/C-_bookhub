using BusinessLayer.Facades.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Mapster;
using MVC.Models.Order;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;

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

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var order = await _orderFacade.CreateOrderAsync(user.UserId);

        return View("OrderCreateView");
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
