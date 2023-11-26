using AutoMapper;
using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.User;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Account;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade bookFacade)
    {
        _userFacade = bookFacade;
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        return Ok(await _userFacade.FetchAllUsersAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        return Ok(await _userFacade.FetchUserAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        await _userFacade.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto) 
    {
        //TODO PW encryption
        var user = await _userFacade.CreateUserAsync(createUserDto);
        return Created(
              new Uri($"{Request.Path}/{user.Id}", UriKind.Relative),
              user
              );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser(long id, CreateUserDto createUserDto)
    {
        //TODO PW encryption stuff
        var user = await _userFacade.UpdateUserAsync(id, createUserDto);
        return Ok(user);
    }
}
