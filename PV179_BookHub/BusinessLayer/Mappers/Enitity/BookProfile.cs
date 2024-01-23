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
                    .MapFrom(y => ExtractPrimaryAuthor(y))
                    );

        CreateMap<BookEntity, DetailedBookViewDto>()
            .ForMember(
                x => x.PrimaryAuthor,
                opts => opts
                    .MapFrom(y => ExtractPrimaryAuthor(y)
                    ))
            .ForMember(
                x => x.CoAuthors,
                opts => opts
                    .MapFrom(y => ExtractSecondaryAuthors(y)
                    ));

        CreateMap<BookFilterDto, BookFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<DetailedBookViewDto, UpdateBookDto>();

    }

    private static AuthorEntity? ExtractPrimaryAuthor(BookEntity bookEntity)
    {
        var authors = bookEntity.Authors;
        var assoc = bookEntity.AuthorBookAssociations?.FirstOrDefault(assoc => assoc.IsPrimary);
        if (authors == null || assoc == null)
        {
            return null;
        }

        return authors.FirstOrDefault(a => a.Id == assoc.AuthorId);
    }

    private static IEnumerable<AuthorEntity>? ExtractSecondaryAuthors(BookEntity bookEntity)
    {
        var authors = bookEntity.Authors;
        var assoc = bookEntity.AuthorBookAssociations?.FirstOrDefault(assoc => assoc.IsPrimary);
        if (authors == null || assoc == null)
        {
            return authors;
        }

        return authors.Where(a => a.Id != assoc.AuthorId).ToList();
    }
}
