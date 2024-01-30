using AutoMapper;
using BusinessLayer.DTOs.Author.Filter;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.BaseFilter;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.Publisher.Filter;
using BusinessLayer.DTOs.Publisher.View;
using BusinessLayer.DTOs.WishList.Filter;
using BusinessLayer.DTOs.WishList.View;
using MVC.Models.Author;
using MVC.Models.Base;
using MVC.Models.Book;
using MVC.Models.Publisher;
using MVC.Models.WishList;

namespace MVC.Mappers;

public class FiltersProfile : Profile
{
    public FiltersProfile()
    {
        // Author Filters
        CreateMap<AuthorSearchModel, AuthorFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == AuthorSortParameters.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralAuthorViewDto>, GenericFilteredModel<AuthorSearchModel, GeneralAuthorViewDto>>();

        // Book Filters
        CreateMap<BookSearchModel, BookFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == BookSortParam.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralBookViewDto>, GenericFilteredModel<BookSearchModel, GeneralBookViewDto>>();

        // Publisher Filters
        CreateMap<PublisherSearchModel, PublisherFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == PublisherSortParameters.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralPublisherViewDto>, GenericFilteredModel<PublisherSearchModel, GeneralPublisherViewDto>>();


        // WishList Filters
        CreateMap<WishListSearchModel, WishListFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == WishListSortParameters.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralWishListViewDto>, GenericFilteredModel<WishListSearchModel, GeneralWishListViewDto>>();
    }
}
