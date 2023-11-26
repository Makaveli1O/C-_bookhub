using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;

namespace BusinessLayer.Facades.Author;

public interface IAuthorFacade
{
    Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
    Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto);
    Task<DetailedAuthorViewDto> FindAuthorByIdAsync(long id);
    Task<IEnumerable<GeneralAuthorViewDto>> GetAllAuthorsAsync();
    Task DeleteAuthorAsync(long id);
}
