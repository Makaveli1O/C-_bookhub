using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;

namespace BusinessLayer.Services.Author;

public interface IAuthorService : IBaseService
{
    Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto, bool save = true);
    Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto, bool save = true);
    Task<List<GeneralAuthorViewDto>> FetchAllAuthorsAsync();
    Task<DetailedAuthorViewDto> FindAuthorByIdAsync(long id);
    Task DeleteAuthorByIdAsync(long id, bool save = true);
}
