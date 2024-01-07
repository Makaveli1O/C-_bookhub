using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;

namespace BusinessLayer.Mappers.Enitity;

public class WishListProfile : Profile
{
    public WishListProfile()
    {
        CreateMap<CreateWishListDto, WishListEntity>();
        CreateMap<WishListEntity, GeneralWishListViewDto>();
    }
}
