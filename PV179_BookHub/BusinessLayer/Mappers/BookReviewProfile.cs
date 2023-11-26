using AutoMapper;
using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Account;

namespace BusinessLayer.Mappers
{
    public class BookReviewProfile : Profile
    {
        public BookReviewProfile()
        {
            CreateMap<CreateBookReviewDto, BookReview>();
            CreateMap<BookReview, GeneralBookReviewViewDto>();
            CreateMap<BookReview, DetailedBookReviewViewDto>();
        }
    }
}
