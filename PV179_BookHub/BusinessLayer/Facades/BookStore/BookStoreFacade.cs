using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Services;

namespace BusinessLayer.Facades.BookStore;

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
        : base(mapper, null, null)
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

        await _addressService.FindByIdAsync(updateBookStoreDto.AddressId);
        bookStore.AddressId = updateBookStoreDto.AddressId;

        await _userService.FindByIdAsync(updateBookStoreDto.ManagerId);
        // in the future we should check that the user has manager role
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
