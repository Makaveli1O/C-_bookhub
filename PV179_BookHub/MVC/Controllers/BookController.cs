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
using MVC.Models.Base;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.Author.View;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class BookController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBookFacade _bookFacade;
    private readonly IPublisherFacade _publisherFacade;
    private readonly IAuthorFacade _authorFacade;

    public BookController(IMapper mapper, IBookFacade bookFacade, IAuthorFacade authorFacade, IPublisherFacade publisherFacade)
    {
        _mapper = mapper;
        _bookFacade = bookFacade;
        _publisherFacade = publisherFacade;
        _authorFacade = authorFacade;
    }

    [AllowAnonymous]

    public async Task<IActionResult> Index(BookSearchModel bookSearchModel)
    {
        var result = await _bookFacade
            .FetchFilteredBooksAsync(
                _mapper.Map<BookFilterDto>(bookSearchModel)
            );
        var viewModel = _mapper.Map<GenericFilteredModel<BookSearchModel, GeneralBookViewDto>>(result);
        viewModel.SearchModel = bookSearchModel;

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

    public async Task<IActionResult> AssignAuthor(long bookId)
    {
        var book = await _bookFacade.FindBookByIdAsync(bookId);
        ViewBag.Authors = new SelectList((await _authorFacade.GetAllAuthorsAsync()).ToList(), "Id", "Name");

        return View(_mapper.Map<AssignAuthorViewModel>(book));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignAuthor(long bookId, AssignAuthorViewModel model)
    {
        var book = await _bookFacade.AssignAuthorToBookAsync(bookId,
                            _mapper.Map<AuthorBookAssociationDto>(model),
                            model.Force);

        return RedirectToAction(nameof(Details), new { id = book.Id, updated = true });
    }

    private List<GeneralAuthorViewDto> GetAuthorsFromBook(DetailedBookViewDto book)
    {
        var authors = new List<GeneralAuthorViewDto>();
        if (book.PrimaryAuthor != null)
        {
            authors.Add(book.PrimaryAuthor);
        }

        if (book.CoAuthors != null)
        {
            authors.AddRange(book.CoAuthors);
        }
        return authors;
    }

    public async Task<IActionResult> UnAssignAuthor(long bookId)
    {
        var book = await _bookFacade.FindBookByIdAsync(bookId);
        ViewBag.Authors = new SelectList(GetAuthorsFromBook(book), "Id", "Name");

        return View(_mapper.Map<UnAssignAuthorViewModel>(book));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnAssignAuthor(long bookId, UnAssignAuthorViewModel model)
    {
        await _bookFacade.UnassignAuthorFromBookAsync(bookId, model.AuthorId);

        return RedirectToAction(nameof(Details), new { id = bookId, updated = true });
    }

    public async Task<IActionResult> MakeUnmakeAuthorPrimary(long bookId)
    {
        var book = await _bookFacade.FindBookByIdAsync(bookId);
        ViewBag.Authors = new SelectList(GetAuthorsFromBook(book), "Id", "Name");

        return View(_mapper.Map<PrimaryAuthorViewModel>(book));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MakeUnmakeAuthorPrimary(long bookId, PrimaryAuthorViewModel model)
    {
        await _bookFacade.MakeUnmakeAuthorPrimaryAsync(bookId, 
            _mapper.Map<AuthorBookAssociationDto>(model), 
            model.Force);

        return RedirectToAction(nameof(Details), new { id = bookId, updated = true });
    }
}
