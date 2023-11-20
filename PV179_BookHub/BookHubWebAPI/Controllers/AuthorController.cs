using AutoMapper;
using BookHubWebAPI.Api.Author.Create;
using BookHubWebAPI.Api.Author.View;
using DataAccessLayer.Models.Publication;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = _mapper.Map<Author>(createAuthorDto);

        await _unitOfWork.AuthorRepository.AddAsync(author);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{author.Id}", UriKind.Relative),
            _mapper.Map<ViewAuthorDto>(author)
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, CreateAuthorDto createAuthorDto)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        author.Name = createAuthorDto.Name ?? author.Name;

        _unitOfWork.AuthorRepository.Update(author);
        await _unitOfWork.CommitAsync();

        return Ok(
            _mapper.Map<ViewAuthorDto>(author)
            );
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var authors = await _unitOfWork.AuthorRepository.GetAllAsync();

        return Ok(
            _mapper.Map<List<ViewAuthorDto>>(authors)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(
            _mapper.Map<ViewAuthorDto>(author)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _unitOfWork.AuthorRepository.Delete(author);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }
}
