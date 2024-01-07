using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.Query.Filters;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Book;

public class BookService : GenericService<BookEntity, long>
{
    public BookService(IUnitOfWork unitOfWork, IQuery<BookEntity, long> query) : base(unitOfWork, query)
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

    public override async Task<QueryResult<BookEntity>> FetchFilteredAsync(IFilter<BookEntity> filter, QueryParams queryParams)
    {
        return await ExecuteQueryAsync(filter, queryParams, book => book.Authors, book => book.Publisher);
    }

    public override async Task<BookEntity> FindByIdAsync(long id)
    {
        var book = await Repository
            .GetByIdAsync(
                id, 
                x => x.Authors, 
                x => x.Reviews, 
                x => x.Publisher,
                x => x.AuthorBookAssociations
            );

        if (book == null)
        {
            throw new NoSuchEntityException<long>(typeof(BookEntity), id);
        }

        return book;
    }
}
