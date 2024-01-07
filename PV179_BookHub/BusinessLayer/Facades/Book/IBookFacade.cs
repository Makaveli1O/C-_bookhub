using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.Update;
using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.Facades.Book;

public interface IBookFacade
{
    Task<DetailedBookViewDto> CreateBookAsync(CreateBookDto createBookDto);
    Task<DetailedBookViewDto> UpdateBookAsync(long id, UpdateBookDto updateBookDto);
    Task<DetailedBookViewDto> AssignAuthorToBookAsync(long id, AuthorBookAssociationDto authorBookAssociation, bool force = false);
    Task UnassignAuthorFromBookAsync(long bookId, long authorId);
    Task<DetailedBookViewDto> MakeUnmakeAuthorPrimaryAsync(long bookId, AuthorBookAssociationDto authorBookAssociation, bool force = false);
    Task<IEnumerable<GeneralBookViewDto>> FetchAllBooksAsync();
    Task<IEnumerable<GeneralBookViewDto>> FetchFilteredBooksAsync(BookFilterDto bookFilterDto);
    Task<DetailedBookViewDto> FindBookByIdAsync(long id);
    Task DeleteBookByIdAsync(long id);
}
