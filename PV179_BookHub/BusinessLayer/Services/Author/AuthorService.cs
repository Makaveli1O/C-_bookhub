using AutoMapper;
using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Author;

public class AuthorService : BaseService<DataAccessLayer.Models.Publication.Author, long>, IAuthorService
{
    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DetailedAuthorViewDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto, bool save = true)
    {
        var author = _mapper.Map<DataAccessLayer.Models.Publication.Author>(createAuthorDto);

        await Repository.AddAsync(author);
        await SaveAsync(save);

        return _mapper.Map<DetailedAuthorViewDto>(author);
    }

    public async Task<DetailedAuthorViewDto> UpdateAuthorAsync(long id, CreateAuthorDto updateAuthorDto, bool save = true)
    {
        var author = await Repository.GetByIdAsync(id);
        if (author == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Author), id);
        }

        author.Name = updateAuthorDto.Name ?? author.Name;
        author.Biography = updateAuthorDto.Biography ?? author.Biography;

        Repository.Update(author);
        await SaveAsync(save);

        return _mapper.Map<DetailedAuthorViewDto>(author);
    }

    public async Task<List<GeneralAuthorViewDto>> FetchAllAuthorsAsync()
    {
        var authors = await Repository.GetAllAsync();
        return _mapper.Map<List<GeneralAuthorViewDto>>(authors);
    }

    public async Task<DetailedAuthorViewDto> FindAuthorByIdAsync(long id)
    {
        var author = await Repository
            .GetByIdAsync(id, x => x.Books);
        if (author == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Author), id);
        }

        return _mapper.Map<DetailedAuthorViewDto>(author);
    }

    public async Task DeleteAuthorByIdAsync(long id, bool save = true)
    {
        var author = await Repository.GetByIdAsync(id);
        if (author == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Author), id);
        }

        Repository.Delete(author);
        await SaveAsync(save);
    }
}
