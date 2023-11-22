using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.Services.Book;

public interface IBookService : IBaseService
{
    Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto, bool save = true);
    Task<DetailedBookViewDto> UpdateBookAsync(long id, CreateBookDto updateBookDto, bool save = true);
    Task<List<GeneralBookViewDto>> FetchAllBooksAsync();
    Task<DetailedBookViewDto> FindBookByIdAsync(long id);
    Task DeleteBookByIdAsync(long id, bool save = true);
}
