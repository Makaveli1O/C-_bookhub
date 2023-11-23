using BusinessLayer.Exceptions;
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
