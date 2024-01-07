using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.Update;
using BusinessLayer.DTOs.Book.View;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Mappers.Enitity;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, BookEntity>();

        CreateMap<BookEntity, GeneralBookViewDto>()
            .ForMember(
                x => x.Author, 
                opts => opts
                    .MapFrom(y => y.Authors
                        .FirstOrDefault(author => author.Id == y.AuthorBookAssociations.FirstOrDefault(z => z.IsPrimary).AuthorId))
                    );

        CreateMap<BookEntity, DetailedBookViewDto>()
            .ForMember(
                x => x.PrimaryAuthor,
                opts => opts
                    .MapFrom(y => y.Authors
                        .FirstOrDefault(author => author.Id == y.AuthorBookAssociations.FirstOrDefault(z => z.IsPrimary).AuthorId))
                    )
            .ForMember(
                x => x.CoAuthors,
                opts => opts
                    .MapFrom(y => y.Authors
                        .Where(author => author.Id != y.AuthorBookAssociations.FirstOrDefault(z => z.IsPrimary).AuthorId))
                    );

        CreateMap<BookFilterDto, BookFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
