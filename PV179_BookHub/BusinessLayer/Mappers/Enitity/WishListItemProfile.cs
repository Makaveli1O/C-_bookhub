using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Preferences;

namespace BusinessLayer.Mappers.Enitity;

public class WishListItemProfile : Profile
{
    public WishListItemProfile()
    {
        CreateMap<CreateWishListItemDto, WishListItemEntity>();
        CreateMap<WishListItemEntity, GeneralWishListItemViewDto>();
    }
}
