using AutoMapper;
using BookHubWebAPI.Api.WishList.Create;
using BookHubWebAPI.Api.WishList.View;
using DataAccessLayer.Models.Preferences;

namespace BookHubWebAPI.Mappers;

public class WishListItemProfile : Profile
{
    public WishListItemProfile() 
    {
        CreateMap<CreateWishListItemDto, WishListItem>();
        CreateMap<WishListItem, GeneralWishListItemViewDto>();
    }
}
