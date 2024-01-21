using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.Facades.Author;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Update;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MVC.Models.Book;
using BusinessLayer.DTOs.Book.Filter;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class BookController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBookFacade _bookFacade;
    private readonly IPublisherFacade _publisherFacade;
    private readonly IAuthorFacade _authorFacade;
    private readonly UserManager<User> _userManager;

    public BookController(IMapper mapper, IBookFacade bookFacade, IAuthorFacade authorFacade, IPublisherFacade publisherFacade, UserManager<User> userManager)
    {
        _mapper = mapper;
        _bookFacade = bookFacade;
        _publisherFacade = publisherFacade;
        _authorFacade = authorFacade;
        _userManager = userManager;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var books = await _bookFacade.FetchAllBooksAsync();
        return View(books);
    }


    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> Filter(SearchBooksModel model)
    {
        var filter = _mapper.Map<BookFilterDto>(model);
        var result = await _bookFacade.FetchFilteredBooksAsync(filter);
        var viewModel = new FilteredBooksModel()
        {
            Books = result.Books,
            PageNumber = result.PageNumber,
            SearchBooksModel = model,
            TotalPages = 100
        };

        return View(viewModel);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, bool updated)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        if (updated)
        {
            ViewBag.Message = "Book Saved Successfully";
        }
        return View(book);
    }

    public async Task<IActionResult> Create()
    {
        var publishers = await _publisherFacade.GetAllPublishersAsync();
        ViewBag.Publishers = new SelectList((await _publisherFacade.GetAllPublishersAsync()).ToList(), "Id", "Name");
        ViewBag.Authors = new SelectList((await _authorFacade.GetAllAuthorsAsync()).ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookModel createBookModel)
    {
        // avoiding mapper here because creating this mapping would introduce dependency of BL on MVC. 
        var dto = new CreateBookDto()
        {
            Title = createBookModel.Title,
            ISBN = createBookModel.ISBN,
            PublisherId = createBookModel.PublisherId,
            BookGenre = createBookModel.BookGenre,
            Description = createBookModel.Description,
            Price = createBookModel.Price,
            AuthorIds = new List<AuthorBookAssociationDto>()
        };
        var found = false;
        if (createBookModel.AuthorIds != null)
        {
            foreach (var id in createBookModel.AuthorIds)
            {
                dto.AuthorIds = dto.AuthorIds.Append(new AuthorBookAssociationDto() { Id = id, IsPrimary = (id == createBookModel.PrimaryAuthorId) });
                found = found || id == createBookModel.PrimaryAuthorId;
            }
        }
        if (!found)
        {
            dto.AuthorIds = dto.AuthorIds.Append(new AuthorBookAssociationDto() { Id = createBookModel.PrimaryAuthorId, IsPrimary = true });
        }

        var created = await _bookFacade.CreateBookAsync(dto);
        return RedirectToAction(nameof(Details), new { created.Id, updated = true });

    }


    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        ViewBag.Publishers = new SelectList((await _publisherFacade.GetAllPublishersAsync()).ToList(), "Id", "Name");
        return View(_mapper.Map<UpdateBookDto>(book));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateBookDto updateBookDto)
    {
        var updated = await _bookFacade.UpdateBookAsync(id, updateBookDto);
        return RedirectToAction(nameof(Details), new { id, updated = true });

    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bookFacade.DeleteBookByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
