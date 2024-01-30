using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.Update;
using BusinessLayer.DTOs.BookReview.View;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.DTOs.Publisher.View;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using MVC.Models.BookReview;
using MVC.Models.Author;
using MVC.Models.InventoryItem;
using MVC.Models.Order;
using MVC.Models.Publisher;
using MVC.Models.User;
using MVC.Models.WishList;

namespace MVC.Mappers
{
    public class MvcDtoProfile : Profile
    {
        public MvcDtoProfile()
        {
            CreateMap<GeneralUserViewDto, UserDetailViewModel>();

            CreateMap<DetailedOrderViewDto, OrderDetailViewModel>();

            CreateMap<DetailedOrderItemViewDto, OrderItemViewModel>();
            CreateMap<DetailedInventoryItemViewDto, OrderAvailableBooksViewModel>();
            CreateMap<GeneralBookStoreViewDto, OrderBookStoresViewModel>();
            CreateMap<DetailedOrderViewDto, OrderRefundViewModel>();
            CreateMap<DetailedOrderViewDto, OrderPaymentViewModel>();
            CreateMap<DetailedOrderViewDto, CancelViewModel>();

            CreateMap<InventoryItemCreateViewModel, CreateInventoryItemDto>();
            CreateMap<DetailedInventoryItemViewDto, InventoryItemCreateViewModel>();
            
            CreateMap<GeneralBookReviewViewDto, BookReviewViewModel>();
            CreateMap<BookReviewCreateViewModel, CreateBookReviewDto>();
            CreateMap<BookReviewEditViewModel, UpdateBookReviewDto>();
            CreateMap<DetailedBookReviewViewDto, GeneralBookReviewViewDto>();
            CreateMap<GeneralBookReviewViewDto, BookReviewEditViewModel>();

            CreateMap<DetailedAuthorViewDto, AuthorDetailViewModel>();

            CreateMap<DetailedPublisherViewDto, PublisherDetailViewModel>();

            CreateMap<CreateWishListViewModel, CreateWishListDto>();
            CreateMap<GeneralWishListViewDto, DetailsWishListModel>();
            CreateMap<GeneralWishListViewDto, UpdateWishListViewModel>();
            CreateMap<CreateWishListItemViewModel, CreateWishListItemDto>();
            CreateMap<GeneralWishListItemViewDto, DetailsWishListItemModel>();
            CreateMap<GeneralWishListItemViewDto, UpdateWishListItemViewModel>()
                .ForMember(x => x.BookId, opt => opt.MapFrom(y => y.Book.Id))
                .ForMember(x => x.BookTitle, opt => opt.MapFrom(y => y.Book.Title))
                .ForMember(x => x.BookISBN, opt => opt.MapFrom(y => y.Book.ISBN));
        }
    }
}
