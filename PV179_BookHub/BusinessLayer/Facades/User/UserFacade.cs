using AutoMapper;
using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.User;

using UserEntity = DataAccessLayer.Models.Account.User;

public class UserFacade : BaseFacade, IUserFacade
{
    private readonly IGenericService<UserEntity, long> _userService;

    public UserFacade(IMapper mapper,
                      IGenericService<UserEntity, long> userService)
        : base(mapper)
    {
        _userService = userService;
    }

    public async Task<GeneralUserViewDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<UserEntity>(createUserDto);
       
        await _userService.CreateAsync(user);

        return _mapper.Map<GeneralUserViewDto>(user);
    }

    public async  Task<GeneralUserViewDto> UpdateUserAsync(long id, CreateUserDto createUserDto)
    {
        var user = await _userService.FindByIdAsync(id);

        user.UserName = createUserDto.UserName ?? user.UserName;
        user.PasswordHash = createUserDto.PasswordHash ?? user.PasswordHash;
        user.Salt = createUserDto.Salt ?? user.Salt;

        await _userService.UpdateAsync(user);

        return _mapper.Map<GeneralUserViewDto>(user);

    }

    public async Task DeleteUserAsync(long id)
    {
        var user = await _userService.FindByIdAsync(id);
        await _userService.DeleteAsync(user);
    }

    public async Task<IEnumerable<GeneralUserViewDto>> FetchAllUsersAsync()
    {
        var users = await _userService.FetchAllAsync();
        return _mapper.Map<List<GeneralUserViewDto>>(users);
    }

    public async Task<GeneralUserViewDto> FetchUserAsync(long id)
    {
        var user = await _userService.FindByIdAsync(id);
        return _mapper.Map<GeneralUserViewDto>(user);
    }
}
