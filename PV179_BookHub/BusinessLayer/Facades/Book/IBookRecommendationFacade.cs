using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.BookRecommendation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.Book;

public interface IBookRecommendationFacade
{
    Task<List<GeneralBookViewDto>> GetBookRecommendationsForUser(long userId);
}
