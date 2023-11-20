using AutoMapper;
using BookHubWebAPI.Api.User.Create;
using BookHubWebAPI.Api.User.View;
using DataAccessLayer.Models.Account;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return Ok(
            _mapper.Map <List<GeneralUserViewDto>>(users));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

        return Ok(
            _mapper.Map<GeneralUserViewDto>(user)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user != null)
        {
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto) 
    {
        //TODO PW encryption
        var user = _mapper.Map<User>(createUserDto);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return Created(
              new Uri($"{Request.Path}/{user.Id}", UriKind.Relative),
              _mapper.Map<GeneralUserViewDto>(user)
              );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser(long id, CreateUserDto createUserDto)
    {
        //TODO PW encryption stuff
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user != null)
        {
            user.UserName = createUserDto.UserName ?? user.UserName;
            user.PasswordHash = createUserDto.PasswordHash ?? user.PasswordHash;
            user.Salt = createUserDto.Salt ?? user.Salt;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CommitAsync();
        }

        return Ok(
            _mapper.Map<GeneralUserViewDto>(user)
            );
    }
}
