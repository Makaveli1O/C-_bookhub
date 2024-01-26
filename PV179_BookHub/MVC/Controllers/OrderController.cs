using BusinessLayer.Facades.Order;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MVC.Models.Order;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Facades.BookStore;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Facades.Book;
using BusinessLayer.DTOs.Order.Create;
using DataAccessLayer.Models.Purchasing;
using AutoMapper;

namespace MVC.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    private readonly IOrderFacade _orderFacade;
    private readonly UserManager<User> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly IInventoryItemFacade _inventoryItemFacade;
    private readonly IBookFacade _bookFacade;
    private readonly IMapper _mapper;

    public OrderController(
        IOrderFacade orderFacade,
        IBookStoreFacade bookStoreFacade,
        IInventoryItemFacade inventoryItemFacade,
        IBookFacade bookFacade,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _bookStoreFacade = bookStoreFacade;
        _orderFacade = orderFacade;
        _inventoryItemFacade = inventoryItemFacade;
        _userManager = userManager;
        _bookFacade = bookFacade;
        _mapper = mapper;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    [Route("{id:long}/Detail")]
    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        var order = await _orderFacade.FindOrderByIdAsync(id);

        var model = _mapper.Map<OrderDetailViewModel>(order);

        return View(model);
    }

    [HttpGet("User/{id:long}")]
    //[Authorize]
    public async Task<IActionResult> SingleUserOrders(long id)
    {
        var orders = await _orderFacade.FetchOrdersByUserIdAsync(id);
        var orderViewModels = orders.ToList();

        return View(orderViewModels);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var availableBooks = await _inventoryItemFacade.GetAllInventoryItems();

        var bookStores = await _bookStoreFacade.GetAllBookStores();

        var viewModel = new OrderCreateViewModel
        {
            OrderItems = new List<OrderItemViewModel>(),
            AvailableBooks = availableBooks.Adapt<IList<OrderAvailableBooksViewModel>>(),
            AvailableBookStores = bookStores.Adapt<IList<OrderBookStoresViewModel>>(),
        };
        
        for (int i = 0; i < viewModel.AvailableBooks.Count(); i++)
        {
            DetailedBookViewDto detailedBookView = await _bookFacade.FindBookByIdAsync(viewModel.AvailableBooks[i].BookId);
            viewModel.AvailableBooks[i].Price = detailedBookView.Price;
            viewModel.AvailableBooks[i].Title = detailedBookView.Title;
        }

        return View(viewModel);
    }
   
    [HttpPost("Create")]
    public async Task<IActionResult> Create(OrderCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            Unauthorized();
        }

        var order = await _orderFacade.CreateOrderAsync(user.UserId);

        AddSelectedItems(model.AddedItems, model.SelectedBookStore, order.Id);

        RemoveSelectedItems(model.RemovedOrderItems);

        return RedirectToAction(nameof(Detail), new { id = order.Id });

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
            OrderItems = _mapper.Map<IList<OrderItemViewModel>>(orderItems),
            AvailableBooks = _mapper.Map<IList<OrderAvailableBooksViewModel>>(availableBooks),
            AvailableBookStores = _mapper.Map<IList<OrderBookStoresViewModel>>(bookStores),
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

        if (user == null || order.UserId != user.Id)
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

        var viewModel = order.Adapt<CancelViewModel>();

        
        return View(viewModel);
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
        if (order.UserId != user.Id)
        {
            Unauthorized();
        }

        await _orderFacade.PayForOrderAsync(id);

        var viewModel = order.Adapt<OrderPaymentViewModel>();

        return View(viewModel);
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
        if (order.UserId != user.Id)
        {
            Unauthorized();
        }

        await _orderFacade.RefundOrderAsync(id);

        var viewModel = order.Adapt<OrderRefundViewModel>();

        return View(viewModel);
    }

}
