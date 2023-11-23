using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using BusinessLayer.Services.Book;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Facades.Book;

public class BookFacade : BaseFacade, IBookFacade
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IGenericService<Publisher, long> _publisherService;

    public BookFacade(IMapper mapper, IBookService bookService, IAuthorService authorService, IGenericService<Publisher, long> publisherService)
        : base(mapper)
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisherService;
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        var publisher = await _publisherService.FindByIdAsync(createBookDto.PublisherId);
        var authors = await _authorService.FetchAllAuthorsByIdsAsync(createBookDto.AuthorIds);

        var book = _mapper.Map<DataAccessLayer.Models.Publication.Book>(createBookDto);
        book.Authors = authors;
        book.Publisher = publisher;
        await _bookService.CreateEntityAsync(book);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto)
    {
        var book = await _bookService.FindByIdAsync(id);

        if (updateBookDto.PublisherId != book.PublisherId)
        {
            book.PublisherId = updateBookDto.PublisherId;
            book.Publisher = null;
        }

        book.Title = updateBookDto.Title ?? book.Title;
        book.ISBN = updateBookDto.ISBN ?? book.ISBN;
        book.BookGenre = updateBookDto.BookGenre;
        book.Description = updateBookDto.Description ?? book.Description;
        book.Price = book.Price;

        var authors = await _authorService.FetchAllAuthorsByIdsAsync(updateBookDto.AuthorIds);
        book.Authors = authors;

        await _bookService.UpdateEntityAsync(book);
        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task<List<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        var books = await _bookService.FetchAllAsync();

        return _mapper.Map<List<GeneralBookViewDto>>(books);
    }
    public async Task<DetailedBookViewDto> FindBookByIdAsync(long id)
    {
        var book = await _bookService.FindByIdAsync(id);

        return _mapper.Map<DetailedBookViewDto>(book);
    }

    public async Task DeleteBookByIdAsync(long id)
    {
        var book = await _bookService.FindByIdAsync(id);

        await _bookService.DeleteEntityAsync(book);
    }
}
