namespace BusinessLayer.Services.BookStore;

public interface IBookStoreService : IGenericService<BookStoreEntity, long>
{
    Task<BookStoreEntity> GetBookStoreByUserIdAsync(long id);
}
