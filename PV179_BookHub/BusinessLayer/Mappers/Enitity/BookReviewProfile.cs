using AutoMapper;
using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Account;

namespace BusinessLayer.Mappers.Enitity
{
    public class BookReviewProfile : Profile
    {
        public BookReviewProfile()
        {
            CreateMap<CreateBookReviewDto, BookReviewEntity>();
            CreateMap<BookReviewEntity, GeneralBookReviewViewDto>();
            CreateMap<BookReviewEntity, DetailedBookReviewViewDto>();
        }
    }
}
