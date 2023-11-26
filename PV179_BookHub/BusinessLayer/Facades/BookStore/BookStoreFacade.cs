using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Services.Author;
using BusinessLayer.Services;
using BusinessLayer.Services.BookStore;

namespace BusinessLayer.Facades.BookStore
{
    public class BookStoreFacade : BaseFacade, IBookStoreFacade
    {
        private readonly IBookStoreService _bookStoreService;
        private readonly IAuthorService _authorService;
        private readonly IGenericService<DataAccessLayer.Models.Publication.Publisher, long> _publisherService;

        public BookStoreFacade(IMapper mapper, IBookStoreService bookStoreService) : base(mapper)
        {
            _bookStoreService = bookStoreService;
        }

        public async Task<IEnumerable<DetailedBookStoreViewDto>> GetAllBookStores()
        {
            var bookStores = await _bookStoreService.FetchAllAsync();
            return _mapper.Map<List<DetailedBookStoreViewDto>>(bookStores);

        }

        public async Task<DetailedBookStoreViewDto> GetBookStore(long id)
        {
            var bookStore = await _bookStoreService.FindByIdAsync(id);
            return _mapper.Map<DetailedBookStoreViewDto>(bookStore);
        }

        public async Task<DetailedBookStoreViewDto> CreateBookStore(CreateBookStoreDto createBookStoreDto)
        {
            var bookStore = _mapper.Map<DataAccessLayer.Models.Logistics.BookStore>(createBookStoreDto);
            await _bookStoreService.CreateAsync(bookStore);
            return _mapper.Map<DetailedBookStoreViewDto>(bookStore);

        }

        public async Task<DetailedBookStoreViewDto> UpdateBookStore(long id, CreateBookStoreDto updateBookStoreDto)
        {
            var bookStore = await _bookStoreService.FindByIdAsync(id);
            if (bookStore != null)
            {
                bookStore.AddressId = updateBookStoreDto.AddressId;
                bookStore.ManagerId = updateBookStoreDto.ManagerId;
                bookStore.Name = updateBookStoreDto.Name;
                bookStore.PhoneNumber = updateBookStoreDto.PhoneNumber;
                bookStore.Email = updateBookStoreDto.Email;
                await _bookStoreService.UpdateAsync(bookStore);
            }
            return _mapper.Map<DetailedBookStoreViewDto>(bookStore);
        }


        public async Task DeleteBookStore(long id)
        {
            var bookStore = await _bookStoreService.FindByIdAsync(id);
            await _bookStoreService.DeleteAsync(bookStore);
        }

    }



    

}
