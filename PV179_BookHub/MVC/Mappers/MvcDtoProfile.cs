using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using MVC.Models.InventoryItem;
using MVC.Models.Order;
using MVC.Models.User;
using MVC.Models.WishList;

namespace MVC.Mappers
{
    public class MvcDtoProfile : Profile
    {
        public MvcDtoProfile()
        {
            CreateMap<WishListCreateViewModel, CreateWishListDto>();
            CreateMap<GeneralWishListItemViewDto, WishListItemViewModel>();
            CreateMap<GeneralBookViewDto, WishListAvailableBooksViewModel>();

            CreateMap<GeneralUserViewDto, UserDetailViewModel>();

            CreateMap<DetailedOrderViewDto, OrderDetailViewModel>();

            CreateMap<DetailedOrderItemViewDto, OrderItemViewModel>();
            CreateMap<DetailedInventoryItemViewDto, OrderAvailableBooksViewModel>();
            CreateMap<GeneralBookStoreViewDto, OrderBookStoresViewModel>();

            CreateMap<InventoryItemCreateViewModel, CreateInventoryItemDto>();
            CreateMap<DetailedInventoryItemViewDto, InventoryItemCreateViewModel>();
        }
    }
}
