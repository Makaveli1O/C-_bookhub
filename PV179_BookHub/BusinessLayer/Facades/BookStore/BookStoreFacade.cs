using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;

namespace BusinessLayer.Facades.BookStore
{
    public class BookStoreFacade : BaseFacade, IBookStoreFacade
    {
        private readonly IGenericService<BookStoreEntity, long> _bookStoreService;
        private readonly IGenericService<UserEntity, long> _userService;
        private readonly IGenericService<AddressEntity, long> _addressService;

        public BookStoreFacade(
            IMapper mapper,
            IGenericService<BookStoreEntity, long> bookStoreService,
            IGenericService<UserEntity, long> userService,
            IGenericService<AddressEntity, long> addressService)
            : base(mapper)
        {
            _bookStoreService = bookStoreService;
            _userService = userService;
            _addressService = addressService;
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

            // these null checks could be moved to the service layer bellow
            var address = await _addressService.FindByIdAsync(updateBookStoreDto.AddressId);
            if (address == null)
            {
                throw new NoSuchEntityException<long>(typeof(AddressEntity), updateBookStoreDto.AddressId);
            }
            bookStore.AddressId = updateBookStoreDto.AddressId;

            var manager = await _userService.FindByIdAsync(updateBookStoreDto.ManagerId);
            if (manager == null)
            {
                throw new NoSuchEntityException<long>(typeof(UserEntity), updateBookStoreDto.ManagerId);
            }
            // we should additionally check that the user has manager role
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
