using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Preferences;

namespace BusinessLayer.Mappers;

public class WishListItemProfile : Profile
{
    public WishListItemProfile() 
    {
        CreateMap<CreateWishListItemDto, WishListItem>();
        CreateMap<WishListItem, GeneralWishListItemViewDto>();
    }
}
