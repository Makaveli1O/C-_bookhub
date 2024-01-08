using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.BookStore;
public class BookStoreService : GenericService<BookStoreEntity, long>, IBookStoreService
{
    public BookStoreService(IUnitOfWork unitOfWork, IQuery<BookStoreEntity, long> query)
        : base(unitOfWork, query)
    {
    }

    public async Task<BookStoreEntity> GetBookStoreByUserIdAsync(long id)
    {
        var bookStore = await Repository.GetSingleAsync(bookStore => bookStore.ManagerId == id);
        
        if (bookStore == null)
        {
            throw new NoSuchEntityException<long>(typeof(BookStoreEntity));
        }
        
        return bookStore;
    }
}
