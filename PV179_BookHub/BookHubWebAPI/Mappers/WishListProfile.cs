using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;

public class WishListProfile : Profile
{
    public WishListProfile() 
    {
        CreateMap<CreateWishListDto, WishList>();
        CreateMap<WishList, GeneralWishListViewDto>();
    }
}
