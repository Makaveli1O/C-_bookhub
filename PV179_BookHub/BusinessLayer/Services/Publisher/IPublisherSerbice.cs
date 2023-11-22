using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.Services.Publisher;

public interface IPublisherSerbice : IBaseService
{
    Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto, bool save = true);
    Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto, bool save = true);
    Task<List<GeneralAuthorViewDto>> FetchAllAuthorsAsync();
    Task<GeneralAuthorViewDto> FindAuthorByIdAsync(long id);
    Task DeleteBookByIdAsync(long id, bool save = true);
}
