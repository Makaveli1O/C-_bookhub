using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.BookRecommendation.View;
using BusinessLayer.Services.Order;
using DataAccessLayer.Models.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Facades.Book;

public class BookRecommendationFacade : BaseFacade, IBookRecommendationFacade
{
    private readonly IOrderService _orderService;
    private readonly IBookFacade _bookFacade;
    public BookRecommendationFacade(
        IMapper mapper,
        IMemoryCache memoryCache,
        IOrderService orderService,
        IBookFacade bookFacade) : base(mapper, memoryCache, "bookRecommendations-")
    {
        _orderService = orderService;
        _bookFacade = bookFacade;
    }

    public async Task<List<GeneralBookViewDto>> GetBookRecommendationsForUser(long userId)
    {
        var orderHistory = await _orderService.FetchAllByUserIdAsync(userId);
        var userProfile = BuildUserProfile(orderHistory);
        var recommendedBooks = await ContentBasedFiltering(userProfile);

        return recommendedBooks;
    }

    private RecommendationUserProfile BuildUserProfile(IEnumerable<OrderEntity> orderHistory)
    {
        var userProfile = new RecommendationUserProfile();

        foreach (var order in orderHistory)
        {
            foreach (var item in order.Items)
            {
                BookGenre bookGenre = item.Book.BookGenre;

                var authors = item.Book.Authors;

                if (bookGenre != null)
                {
                    userProfile.PreferredGenres.Add(bookGenre);
                }

                foreach (var author in authors)
                {
                    userProfile.PreferredAuthors.Add(author.Id);
                }
            }
        }
        
        return userProfile;
    }

    private async Task<List<GeneralBookViewDto>> ContentBasedFiltering(RecommendationUserProfile userProfile)
    {
        var recommendedBooks = new List<GeneralBookViewDto>();
        var availableBooks = await _bookFacade.FetchAllBooksAsync();
        
        foreach (var book in availableBooks)
        {
            if (userProfile.PreferredGenres.Contains(book.BookGenre))
            {
                recommendedBooks.Add(book);
                continue;
            }

            if(book.Author != null)
            {
                if (userProfile.PreferredAuthors.Contains(book.Author.Id))
                {
                    recommendedBooks.Add(book);
                }
            }
        }

        return recommendedBooks;
    }
}
