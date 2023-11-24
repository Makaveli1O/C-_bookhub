using BusinessLayer.Exceptions;
using Infrastructure.NaiveQuery;
using Infrastructure.NaiveQuery.Filters.EnitityFilters;
using Infrastructure.UnitOfWork;


namespace BusinessLayer.Services.Book;

public class BookService : GenericService<DataAccessLayer.Models.Publication.Book, long>, IBookService
{
    public BookService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<IEnumerable<DataAccessLayer.Models.Publication.Book>> FetchAllAsync()
    {
        var books = await Repository
            .GetAllAsync(
                null,
                book => book.Authors,
                book => book.Publisher
            );

        return books;
    }

    public async Task<QueryResult<DataAccessLayer.Models.Publication.Book>> 
        FetchFilteredBooksAsync(BookFilter bookFilter, int? pageNumber, int? pageSize, string? sortParam, bool? asc)
    {
        {
            QueryBase<DataAccessLayer.Models.Publication.Book, long> query =
                new QueryBase<DataAccessLayer.Models.Publication.Book, long>(_unitOfWork)
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

    public override async Task<DataAccessLayer.Models.Publication.Book> FindByIdAsync(long id)
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
            throw new NoSuchEntityException<long>(typeof(DataAccessLayer.Models.Publication.Book), id);
        }

        return book;
    }
}
