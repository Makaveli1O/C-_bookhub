using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Logistics;

namespace BusinessLayer.Mappers;
public class BookStoreProfile : Profile
{
    public BookStoreProfile()
    {
        CreateMap<CreateBookStoreDto, BookStore>();
        CreateMap<BookStore, DetailedBookStoreViewDto>();

        CreateMap<CreateInventoryItemDto, InventoryItem>();
        CreateMap<InventoryItem, DetailedInventoryItemViewDto>();
    }
}
