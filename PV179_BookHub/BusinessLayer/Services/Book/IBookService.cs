using Infrastructure.NaiveQuery;
using Infrastructure.NaiveQuery.Filters.EnitityFilters;

namespace BusinessLayer.Services.Book;

public interface IBookService : IGenericService<DataAccessLayer.Models.Publication.Book, long>
{
    Task<QueryResult<DataAccessLayer.Models.Publication.Book>> FetchFilteredBooksAsync(
        BookFilter bookFilter, int? pageNumber, int? pageSize, string? sortParam, bool? asc);
}
