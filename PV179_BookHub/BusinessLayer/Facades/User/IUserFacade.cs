using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.User;

public interface IUserFacade
{
    Task<GeneralUserViewDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<GeneralUserViewDto> UpdateUserAsync(long id, CreateUserDto createUserDto);
    Task DeleteUserAsync(long id);
    Task<IEnumerable<GeneralUserViewDto>> FetchAllUsersAsync();
    Task<GeneralUserViewDto> FetchUserAsync(long id);
}
