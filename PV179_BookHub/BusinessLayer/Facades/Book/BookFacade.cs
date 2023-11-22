using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.Services.Book;

namespace BusinessLayer.Facades.Book;

public class BookFacade : IBookFacade
{
    private readonly IBookService _bookService;
    public BookFacade(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        return await _bookService.CreateBookAsync(createBookDto);
    }

    public async Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto)
    {
        return await _bookService.UpdateBookAsync(id, updateBookDto);
    }

    public async Task<List<GeneralBookViewDto>> FetchAllBooksAsync()
    {
        return await _bookService.FetchAllBooksAsync();
    }
    public async Task<DetailedBookViewDto> FindBookByIdAsync(long id)
    {
        return await _bookService.FindBookByIdAsync(id);
    }

    public async Task DeleteBookByIdAsync(long id)
    {
        await _bookService.DeleteBookByIdAsync(id);
    }
}
