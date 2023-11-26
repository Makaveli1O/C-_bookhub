using BusinessLayer.Exceptions;
using Infrastructure.NaiveQuery;
using Infrastructure.NaiveQuery.Filters.EntityFilters;
using Infrastructure.UnitOfWork;


namespace BusinessLayer.Services.Book;

public class BookService : GenericService<BookEntity, long>, IBookService
{
    public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<IEnumerable<BookEntity>> FetchAllAsync()
    {
        var books = await Repository
            .GetAllAsync(
                null,
                book => book.Authors,
                book => book.Publisher
            );

        return books;
    }

    public async Task<QueryResult<BookEntity>> 
        FetchFilteredBooksAsync(BookFilter bookFilter, int? pageNumber, int? pageSize, string? sortParam, bool? asc)
    {
        {
            QueryBase<BookEntity, long> query =
                new QueryBase<BookEntity, long>(_unitOfWork)
                {
                    Filter = bookFilter,
                };

            query.CurrentPage = pageNumber ?? query.CurrentPage;
            query.PageSize = pageSize ?? query.PageSize;
            query.SortAccordingTo = sortParam ?? query.SortAccordingTo;
            query.UseAscendingOrder = asc ?? query.UseAscendingOrder;

            query.Include(book => book.Authors, book => book.Publisher);
            query.Where(query.Filter.CreateExpression());
            query.Page(query.CurrentPage, query.PageSize);
            query.SortBy(query.SortAccordingTo, query.UseAscendingOrder);

            return await query.ExecuteAsync();
        }
    }

    public override async Task<BookEntity> FindByIdAsync(long id)
    {
        var book = await Repository
            .GetByIdAsync(
                id, 
                x => x.Authors, 
                x => x.Reviews, 
                x => x.Publisher
            );

        if (book == null)
        {
            throw new NoSuchEntityException<long>(typeof(BookEntity), id);
        }

        return book;
    }
}
