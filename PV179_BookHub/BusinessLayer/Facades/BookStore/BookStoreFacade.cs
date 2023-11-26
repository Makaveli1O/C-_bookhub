using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Services.BookStore;

namespace BusinessLayer.Facades.BookStore
{
    public class BookStoreFacade : BaseFacade, IBookStoreFacade
    {
        private readonly IBookStoreService _bookStoreService;

        public BookStoreFacade(IMapper mapper, IBookStoreService bookStoreService) : base(mapper)
        {
            _bookStoreService = bookStoreService;
        }

        public async Task<IEnumerable<DetailedBookStoreViewDto>> GetAllBookStores()
        {
            return _mapper.Map<List<DetailedBookStoreViewDto>>(await _bookStoreService.FetchAllAsync());

        }

        public async Task<DetailedBookStoreViewDto> GetBookStore(long id)
        {
            return _mapper.Map<DetailedBookStoreViewDto>(await _bookStoreService.FindByIdAsync(id));
        }

        public async Task<DetailedBookStoreViewDto> CreateBookStore(CreateBookStoreDto createBookStoreDto)
        {
            return _mapper.Map<DetailedBookStoreViewDto>(
                await _bookStoreService.CreateAsync(
                    _mapper.Map<BookStoreEntity>(createBookStoreDto)));
        }

        public async Task<DetailedBookStoreViewDto> UpdateBookStore(long id, CreateBookStoreDto updateBookStoreDto)
        {
            var bookStore = await _bookStoreService.FindByIdAsync(id);
            bookStore.AddressId = updateBookStoreDto.AddressId;
            bookStore.ManagerId = updateBookStoreDto.ManagerId;
            bookStore.Name = updateBookStoreDto.Name;
            bookStore.PhoneNumber = updateBookStoreDto.PhoneNumber;
            bookStore.Email = updateBookStoreDto.Email;
            await _bookStoreService.UpdateAsync(bookStore);
            return _mapper.Map<DetailedBookStoreViewDto>(bookStore);
        }


        public async Task DeleteBookStore(long id)
        {
            await _bookStoreService.DeleteAsync(await _bookStoreService.FindByIdAsync(id));
        }
    }



    

}
