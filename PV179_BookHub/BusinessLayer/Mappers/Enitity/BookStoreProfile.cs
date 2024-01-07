using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Logistics;

namespace BusinessLayer.Mappers.Enitity;
public class BookStoreProfile : Profile
{
    public BookStoreProfile()
    {
        CreateMap<CreateBookStoreDto, BookStoreEntity>();
        CreateMap<BookStoreEntity, DetailedBookStoreViewDto>();

        CreateMap<CreateInventoryItemDto, InventoryItemEntity>();
        CreateMap<InventoryItemEntity, DetailedInventoryItemViewDto>();
    }
}
