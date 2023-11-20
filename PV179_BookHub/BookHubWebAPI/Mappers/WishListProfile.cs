using AutoMapper;
using BookHubWebAPI.Api.WishList.Create;
using BookHubWebAPI.Api.WishList.View;
using DataAccessLayer.Models.Preferences;

namespace BookHubWebAPI.Mappers;

public class WishListProfile : Profile
{
    public WishListProfile() 
    {
        CreateMap<CreateWishListDto, WishList>();
        CreateMap<WishList, GeneralWishListViewDto>();
    }
}
