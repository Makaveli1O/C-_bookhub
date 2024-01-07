using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Preferences;

namespace BusinessLayer.Mappers;

public class WishListProfile : Profile
{
    public WishListProfile() 
    {
        CreateMap<CreateWishListDto, WishList>();
        CreateMap<WishList, GeneralWishListViewDto>();
    }
}
