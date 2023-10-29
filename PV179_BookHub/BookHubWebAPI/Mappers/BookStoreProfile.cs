using AutoMapper;
using BookHubWebAPI.Api.BookStore.Create;
using BookHubWebAPI.Api.BookStore.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;
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
