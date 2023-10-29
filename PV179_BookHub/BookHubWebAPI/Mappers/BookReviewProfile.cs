using AutoMapper;
using BookHubWebAPI.Api.BookReview.Create;
using BookHubWebAPI.Api.BookReview.View;
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
