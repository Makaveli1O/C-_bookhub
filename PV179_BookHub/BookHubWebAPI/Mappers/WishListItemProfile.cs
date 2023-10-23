using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;

public class WishListItemProfile : Profile
{
    public WishListItemProfile() 
    {
        CreateMap<CreateWishListItemDto, WishListItem>();
        CreateMap<WishListItem, GeneralWishListItemViewDto>();
    }
}
