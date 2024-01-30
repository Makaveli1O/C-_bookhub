using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.Update;
using BusinessLayer.DTOs.BookReview.View;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.BookStore;
using BusinessLayer.Facades.User;
using DataAccessLayer.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.BookReview;

namespace MVC.Controllers;

[Route("BookReview")]
public class BookReviewController : Controller
{
    private readonly IBookReviewFacade _bookReviewFacade;
    private readonly IBookFacade _bookFacade;
    private readonly IUserFacade _userFacade;
    private readonly IInventoryItemFacade _inventoryItemFacade;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public BookReviewController(
        IBookReviewFacade bookReviewFacade,
        IBookFacade bookFacade,
        IUserFacade userFacade,
        IInventoryItemFacade inventoryItemFacade,
        UserManager<User> userManager,
        IMapper mapper
        )
    {
        _bookReviewFacade = bookReviewFacade;
        _bookFacade = bookFacade;
        _userFacade = userFacade;
        _inventoryItemFacade = inventoryItemFacade;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet("User/{userId:long}")]
    [AllowAnonymous]
    public async Task<IActionResult> SingleUserBookReviews(long userId)
    {
        var userReviews = await _bookReviewFacade.FindUserReviewsAsync(userId);

        List<BookReviewViewModel> model = new List<BookReviewViewModel>();
        foreach (var userReview in userReviews)
        {
            var modelElement = _mapper.Map<BookReviewViewModel>(userReview);
            modelElement.Title = (await _bookFacade.FindBookByIdAsync(userReview.BookId)).Title;
            modelElement.ReviewerName = (await _userFacade.FetchUserAsync(userReview.ReviewerId)).UserName;
            model.Add(modelElement);
        }

        return View("UserReviews", model);
    }

    [HttpGet("Book/{id:long}")]
    public async Task<IActionResult> BookReviews(long id)
    {
        var bookReviews = await _bookReviewFacade.FindBookReviewsAsync(id);

        List<BookReviewViewModel> model = new List<BookReviewViewModel>();
        foreach (var bookReview in bookReviews)
        {
            var modelElement = _mapper.Map<BookReviewViewModel>(bookReview);
            modelElement.Title = (await _bookFacade.FindBookByIdAsync(bookReview.BookId)).Title;
            modelElement.ReviewerName = (await _userFacade.FetchUserAsync(bookReview.ReviewerId)).UserName;
            model.Add(modelElement);
        }

        return View(model);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var availableBooks = await _inventoryItemFacade.GetUniqueInventoryItems();


        var viewModel = new BookReviewCreateViewModel
        {
            ReviewerId = user.Id
        };

        foreach (var book in availableBooks)
        {
            var bookTitle = (await _bookFacade.FindBookByIdAsync(book.Id))?.Title;

            if (bookTitle != null)
            {
                viewModel.AvailableBooks.Add(
                    new BookReviewAvailableBooksViewModel
                    {
                        BookId = book.Id,
                        Title = bookTitle
                    }
                );
            }
        }

        return View(viewModel);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(BookReviewCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createBookReviewDto = _mapper.Map<CreateBookReviewDto>(model);
        createBookReviewDto.BookId = model.SelectedBook;

        var bookView = await _bookReviewFacade.CreateBookReview(createBookReviewDto);

        return View(nameof(Detail) ,_mapper.Map<GeneralBookReviewViewDto>(bookView));
    }

    [HttpGet("Edit/{id:long}")]
    public async Task<IActionResult> Edit(long id)
    {
        var bookReview = await _bookReviewFacade.FetchBookReviewAsync(id);
        if (!await IsAuthorized(bookReview))
        {
            return Unauthorized();
        }

        var model = _mapper.Map<BookReviewEditViewModel>(bookReview);
        model.BookTitle = (await _bookFacade.FindBookByIdAsync(bookReview.BookId)).Title;

        return View("EditReview",model);
    }

    [HttpPost("EditById")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BookReviewEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updateDto = _mapper.Map<UpdateBookReviewDto>(model);

        var generalViewDto = await _bookReviewFacade.UpdateBookReview(model.ReviewerId, updateDto);

        var modelView = _mapper.Map<GeneralBookReviewViewDto>(generalViewDto);

        return View(nameof(Detail), modelView);
    }

    [HttpGet("Delete/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var bookReview = await _bookReviewFacade.FetchBookReviewAsync(id);

        if (!await IsAuthorized(bookReview))
        {
            return Unauthorized();
        }

        await _bookReviewFacade.DeleteBookReviewAsync(id);

        return View();
    }

    [HttpGet("Detail/{id:long}")]
    [AllowAnonymous]
    public async Task<IActionResult> Detail(long id)
    {
        var bookReview = await _bookReviewFacade.FetchBookReviewAsync(id);

        return View(bookReview);
    }

    private async Task<bool> IsAuthorized(GeneralBookReviewViewDto review)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null || review == null)
            return false;

        if (user.Role == DataAccessLayer.Models.Enums.UserRole.Admin)
            return true;

        return review.ReviewerId == user.Id;
    }
}
