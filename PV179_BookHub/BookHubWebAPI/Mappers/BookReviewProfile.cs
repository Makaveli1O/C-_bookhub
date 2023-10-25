using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers
{
    public class BookReviewProfile : Profile
    {
        public BookReviewProfile()
        {
            CreateMap<CreateBookReviewDto, BookReview>();
            CreateMap<BookReview, GeneralBookReviewViewDto>();
        }
    }
}
