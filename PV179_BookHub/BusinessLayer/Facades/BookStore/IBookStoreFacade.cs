using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;

namespace BusinessLayer.Facades.BookStore
{
    public interface IBookStoreFacade
    {
        Task<DetailedBookStoreViewDto> CreateBookStore(CreateBookStoreDto createBookStoreDto);
        Task<DetailedBookStoreViewDto> UpdateBookStore(long id, CreateBookStoreDto updateBookStoreDto);
        Task<IEnumerable<DetailedBookStoreViewDto>> GetAllBookStores();
        Task<DetailedBookStoreViewDto> GetBookStore(long id);
        Task DeleteBookStore(long id);
    }
}
