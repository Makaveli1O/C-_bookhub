using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.Facades.Book;

public interface IBookFacade
{
    Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto);
    Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto);
    Task<List<GeneralBookViewDto>> FetchAllBooksAsync();
    Task<DetailedBookViewDto> FindBookByIdAsync(long id);
    Task DeleteBookByIdAsync(long id);
}
