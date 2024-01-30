using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedData;
using TestUtilities.MockedObjects;
using NSubstitute.ExceptionExtensions;
using BusinessLayer.Services.Book;
using DataAccessLayer.Models.Preferences;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models.Publication;
using BusinessLayer.Services.WishList;
using BusinessLayer.Services.WishListItem;

namespace BusinessLayer.Tests.FacadeTests;


public class WishListFacadeTests
{
	private MockedDependencyInjectionBuilder _serviceProviderBuilder;
	private readonly IWishListService _wishListServiceMock;
	private readonly IWishListItemService _wishListItemServiceMock;
	private readonly IGenericService<Book, long> _bookServiceMock;

	public WishListFacadeTests()
	{
		_serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _wishListServiceMock = Substitute.For<IWishListService>();
		_wishListItemServiceMock = Substitute.For<IWishListItemService>();
		_bookServiceMock = Substitute.For<IGenericService<Book, long>>();
	}

	private ServiceProvider CreateServiceProvider()
	{
		return _serviceProviderBuilder
			.AddScoped(_wishListServiceMock)
			.AddScoped(_wishListItemServiceMock)
			.AddScoped(_bookServiceMock)
			.Create();
	}

	[Fact]
	public async Task CreateWishList_ShouldReturnNewWishList()
	{
		var wishlist = TestDataInitializer.GetTestWishLists().ElementAt(0);

		var wishlistCreateDto = new CreateWishListDto(){UserId = wishlist.UserId};

		_wishListServiceMock.CreateAsync(Arg.Any<WishList>()).Returns(wishlist);

		var serviceProvider = CreateServiceProvider();
		using (var scope = serviceProvider.CreateScope())
		{
			var wishlistFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
			var result = await wishlistFacade.CreateWishListAsync(wishlistCreateDto);

			Assert.NotNull(result);
			Assert.Equal(wishlist.Id, result.Id);
			Assert.Equal(wishlist.UserId, result.UserId);
			Assert.Equal(wishlist.Description, result.Description);
			Assert.Equal(wishlist.CreatedAt, result.CreatedAt);
			await _wishListServiceMock.Received(1).CreateAsync((Arg.Any<WishList>()));
		}
	}

    [Fact]
    public async Task UpdateNonExistingWishList_ShouldThrowExceptionWishListDoesNotExist()
    {
        _wishListServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(WishList), 1));

        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var wishlistFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await wishlistFacade.UpdateWishListAsync(1, "desc"));
            await _wishListServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
	public async Task CreateWishListItem_ShouldReturnNewWishListItem()
	{
		var wishListItem = TestDataInitializer.GetTestWishListItems().ElementAt(0);
		var book = TestDataInitializer.GetTestBooks().First(x => x.Id == wishListItem.BookId);

		_wishListItemServiceMock.CreateAsync(Arg.Any<WishListItem>()).Returns(wishListItem);
		_bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);

		var serviceProvider = CreateServiceProvider();
		using (var scope = serviceProvider.CreateScope())
		{
			var wishListItemFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
			var result = await wishListItemFacade.CreateWishListItemAsync(new CreateWishListItemDto());

			Assert.NotNull(result);
			Assert.Equal(wishListItem.Id, result.Id);
			Assert.Equal(wishListItem.WishListId, result.WishListId);
			Assert.Equal(wishListItem.BookId, result.Book.Id);

			await _wishListItemServiceMock.Received(1).CreateAsync((Arg.Any<WishListItem>()));
			await _bookServiceMock.Received(1).FindByIdAsync((Arg.Any<long>()));
		}
	}

	[Fact]
	public async Task CreateWishListItemNonExistingBook_ShouldThrowExceptionWishListDoesNotExist()
	{
		_bookServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(Book), 0));

		var serviceProvider = CreateServiceProvider();
		using (var scope = serviceProvider.CreateScope())
		{
			var wishlistFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
			await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await wishlistFacade.CreateWishListItemAsync(new CreateWishListItemDto()));
			await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
			await _wishListItemServiceMock.DidNotReceiveWithAnyArgs().CreateAsync(Arg.Any<WishListItem>());
		}
	}

    [Fact]
    public async Task UpdateWishListItem_ShouldReturnUpdatedWishList()
    {
        var wishListItem = TestDataInitializer.GetTestWishListItems().ElementAt(0);
        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == wishListItem.BookId);

        _wishListItemServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(wishListItem);
        _wishListItemServiceMock.UpdateAsync(Arg.Any<WishListItem>()).Returns(wishListItem);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);


        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var wishListItemFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
            var result = await wishListItemFacade.UpdateWishListItemAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(wishListItem.Id, result.Id);
            Assert.Equal(wishListItem.WishListId, result.WishListId);
            Assert.Equal(wishListItem.BookId, result.Book.Id);

            await _wishListItemServiceMock.Received(1).FindByIdAsync((Arg.Any<long>()));
            await _wishListItemServiceMock.Received(1).UpdateAsync((Arg.Any<WishListItem>()));

        }
    }

    [Fact]
    public async Task UpdateNonExistingWishListItem_ShouldThrowExceptionWishListDoesNotExist()
    {
        _wishListItemServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(WishListItem), 1));

        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var wishlistFacade = scope.ServiceProvider.GetRequiredService<IWishListFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await wishlistFacade.UpdateWishListItemAsync(1,1));
            await _wishListItemServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _wishListItemServiceMock.DidNotReceiveWithAnyArgs().UpdateAsync(Arg.Any<WishListItem>());
        }
    }


}

