﻿using AutoMapper;
using BusinessLayer.DTOs.Book.Filter;
using MVC.Models.Book;

namespace MVC.Mappers;

public class FilterBookProfile : Profile
{
    public FilterBookProfile()
    {
        CreateMap<BookSearchModel, BookFilterDto>()
            .ForMember(x => x.SortParameter, 
                    opt => opt
                        .MapFrom(y => 
                            y.SortParameter == BookSortParam.None ? null : y.SortParameter.ToString())
                    );

        CreateMap<BookFilterResultDto, FilteredBooksModel>()
            .ForMember(x => x.TotalPages,
                opt => opt.MapFrom(y => (int)Math.Ceiling((double)y.TotalItemsCount / y.PageSize)));
    }
}
