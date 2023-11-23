using AutoMapper;
using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.Services.Author;

namespace BusinessLayer.Facades.Author;

public class AuthorFacade : BaseFacade, IAuthorFacade
{
    private readonly IAuthorService _authorService;
    public AuthorFacade(IMapper mapper, IAuthorService authorService) : base(mapper)
    {
        _authorService = authorService;
    }

    public async Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
    {
        var author = _mapper.Map<DataAccessLayer.Models.Publication.Author>(createAuthorDto);

        await _authorService.CreateEntityAsync(author);

        return _mapper.Map<DetailedAuthorViewDto>(author);
    }

    public async Task DeleteAuthorAsync(long id)
    {
        var author = await _authorService.FindByIdAsync(id);

        await _authorService.DeleteEntityAsync(author);
    }

    public async Task<DetailedAuthorViewDto> FindAuthorByIdAsync(long id)
    {
        var author = await _authorService.FindByIdAsync(id);

        return _mapper.Map<DetailedAuthorViewDto>(author);
    }

    public async Task<IEnumerable<GeneralAuthorViewDto>> GetAllAuthorsAsync()
    {
        return _mapper.Map<List<GeneralAuthorViewDto>>(await _authorService.FetchAllAsync());
    }

    public async Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto)
    {
        var author = await _authorService.FindByIdAsync(id);

        author.Name = updateAuthorDto.Name ?? author.Name;
        author.Biography = updateAuthorDto.Biography ?? author.Biography;

        await _authorService.UpdateEntityAsync(author);
        return _mapper.Map<DetailedAuthorViewDto>(author);
    }
}
