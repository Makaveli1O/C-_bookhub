using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;

namespace BusinessLayer.Mappers.Enitity;
public class BookStoreProfile : Profile
{
    public BookStoreProfile()
    {
        CreateMap<CreateBookStoreDto, BookStoreEntity>();
        CreateMap<BookStoreEntity, DetailedBookStoreViewDto>();
        CreateMap<BookStoreEntity, GeneralBookStoreViewDto>();
        CreateMap<DetailedBookStoreViewDto, CreateBookStoreDto>();

        CreateMap<CreateInventoryItemDto, InventoryItemEntity>();
        CreateMap<InventoryItemEntity, DetailedInventoryItemViewDto>();
    }
}
