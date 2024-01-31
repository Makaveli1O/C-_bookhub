using AutoMapper;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.Filter;
using BusinessLayer.DTOs.WishList.View;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Mappers.Enitity;

public class WishListProfile : Profile
{
    public WishListProfile()
    {
        CreateMap<CreateWishListDto, WishListEntity>();
        CreateMap<WishListEntity, GeneralWishListViewDto>();

        CreateMap<WishListFilterDto, WishListFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
