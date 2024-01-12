using BusinessLayer.Facades.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Mapster;
using MVC.Models.Order;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Facades.BookStore;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Facades.Book;
using BusinessLayer.DTOs.Order.Create;

namespace MVC.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    private readonly IOrderFacade _orderFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly IInventoryItemFacade _inventoryItemFacade;
    private readonly IBookFacade _bookFacade;

    public OrderController(
        IOrderFacade orderFacade,
        IBookStoreFacade bookStoreFacade,
        IInventoryItemFacade inventoryItemFacade,
        IBookFacade bookFacade,
        UserManager<LocalIdentityUser> userManager)
    {
        _bookStoreFacade = bookStoreFacade;
        _orderFacade = orderFacade;
        _inventoryItemFacade = inventoryItemFacade;
        _userManager = userManager;
        _bookFacade = bookFacade;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    [Route("{id:long}/Detail")]
    [HttpGet]
    public async Task<JsonResult> Detail(long id)
    {
        //would be nicer if we had DetailedOrderItem here instead of the General one
        var order = await _orderFacade.FindOrderByIdAsync(id);

        var model = order.Adapt<OrderDetailViewModel>();

        return Json(model, _jsonSerializerOptions);
    }

    [HttpGet("User/{id:long}")]
    //[Authorize]
    public async Task<JsonResult> SingleUserOrders(long id)
    {
        return Json(await _orderFacade.FetchOrdersByUserIdAsync(id));
    }

    [HttpGet("Create")]
    public async Task<IActionResult> CreateSelf()
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

        var orderCreateResult = await _orderFacade.CreateOrderAsync(user.UserId);

        return RedirectToAction(nameof(Edit), new { id = orderCreateResult.Id });

    }
   
    [HttpPost("Create/{userId:long}")]
    public async Task<IActionResult> Create(long userId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var orderCreateResult = await _orderFacade.CreateOrderAsync(userId);

        return RedirectToAction(nameof(Edit), new { id = orderCreateResult.Id });

    }

    [HttpGet("{id:long}/Edit")]
    public async Task<IActionResult> Edit(long id)
    {
        var orderItems = await _orderFacade.FetchAllItemsFromOrderAsync(id);

        var availableBooks = await _inventoryItemFacade.GetAllInventoryItems();

        var bookStores = await _bookStoreFacade.GetAllBookStores();

        // mapping
        var viewModel = new OrderEditViewModel
        {
            OrderItems = orderItems.Adapt<IList<OrderItemViewModel>>(),
            AvailableBooks = availableBooks.Adapt<IList<OrderAvailableBooksViewModel>>(),
            AvailableBookStores = bookStores.Adapt<IList<OrderBookStoresViewModel>>(),
        };
        // more mappping
        for(int i = 0; i < viewModel.AvailableBooks.Count(); i++)
        {
            DetailedBookViewDto detailedBookView = await _bookFacade.FindBookByIdAsync(viewModel.AvailableBooks[i].BookId);
            viewModel.AvailableBooks[i].Price = detailedBookView.Price;
            viewModel.AvailableBooks[i].Title = detailedBookView.Title;
        }

        return View(viewModel);

    }


    [HttpPost("{orderId:long}/Edit")]
    public async Task<IActionResult> Edit(long orderId, OrderEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Model Error: {error.ErrorMessage}");
            }

            return BadRequest(ModelState);
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            Unauthorized();
        }

        AddSelectedItems(model.AddedItems, model.SelectedBookStore, orderId);

        RemoveSelectedItems(model.RemovedOrderItems);

        return RedirectToAction(nameof(Detail), new { id = orderId });
    }

    private async void AddSelectedItems(Dictionary<int, uint> itemIdsToAdd, int bookStore, long orderId)
    {
        foreach (var (bookId, quantity) in itemIdsToAdd)
        {
            var createOrderItemDto = new CreateOrderItemDto
            {
                BookId = bookId,
                BookStoreId = bookStore,
                Quantity = quantity
            };
            await _orderFacade.CreateOrderItem(orderId, createOrderItemDto);
        }
    }

    private async void RemoveSelectedItems(List<int> itemIdsToDelete)
    {
        foreach (var itemId in itemIdsToDelete)
        {
            await _orderFacade.DeleteOrderItemByIdAsync(itemId);
        }
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

		return Ok();
    }

	//[Authorize]
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

        await _orderFacade.CancelOrderAsync(id);

        return Ok();
    }

    [Route("{id:long}/Pay")]
    public async Task<IActionResult> Pay(long id)
    {
        var order = await _orderFacade.FindOrderByIdAsync(id);

        if (order.State != DataAccessLayer.Models.Enums.OrderState.Created)
        {
            BadRequest();
        }

        var user = await _userManager.GetUserAsync(User);
        if (order.UserId != user.UserId)
        {
            Unauthorized();
        }

        await _orderFacade.PayForOrderAsync(id);

        return Ok();
    }

    [Route("{id:long}/Refund")]
    public async Task<IActionResult> Refund(long id)
    {
        var order = await _orderFacade.FindOrderByIdAsync(id);

        if (order.State != DataAccessLayer.Models.Enums.OrderState.Paid)
        {
            BadRequest();
        }

        var user = await _userManager.GetUserAsync(User);
        if (order.UserId != user.UserId)
        {
            Unauthorized();
        }

        await _orderFacade.RefundOrderAsync(id);

        return Ok();
    }
}
