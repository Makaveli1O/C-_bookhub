using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.Book.Filter;

public record BookFilterDto(string? Title, string? Author, string? Publisher, string? Description,
        BookGenre? BookGenre, double? LEQPrice, double? GEQPrice, string? SortParam, bool? Asc, 
        int PageNumber, int? PageSize);
