using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.Filter;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.BaseFilter;

namespace BusinessLayer.Facades.Author;

public interface IAuthorFacade
{
    Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
    Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto);
    Task<DetailedAuthorViewDto> FindAuthorByIdAsync(long id);
    Task<IEnumerable<GeneralAuthorViewDto>> GetAllAuthorsAsync();
    Task<FilterResultDto<GeneralAuthorViewDto>> FetchFilteredAuthorsAsync(AuthorFilterDto authorFilterDto);
    Task DeleteAuthorAsync(long id);
}
