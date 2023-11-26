using Infrastructure.NaiveQuery;
using Infrastructure.NaiveQuery.Filters.EntityFilters;

namespace BusinessLayer.Services.Book;

public interface IBookService : IGenericService<BookEntity, long>
{
    Task<QueryResult<BookEntity>> FetchFilteredBooksAsync(
        BookFilter bookFilter, int? pageNumber, int? pageSize, string? sortParam, bool? asc);
}
