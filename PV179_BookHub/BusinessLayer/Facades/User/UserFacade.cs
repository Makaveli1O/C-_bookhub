using AutoMapper;
using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using BusinessLayer.Services.Order;

namespace BusinessLayer.Facades.User;

public class UserFacade : BaseFacade, IUserFacade
{
    private readonly IGenericService<UserEntity, long> _userService;
    private readonly IOrderService _orderService;

    public UserFacade(IMapper mapper,
                      IGenericService<UserEntity, long> userService,
                      IOrderService orderService)
        : base(mapper)
    {
        _userService = userService;
        _orderService = orderService;
    }

    public async Task<GeneralUserViewDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<UserEntity>(createUserDto);
       
        user = await _userService.CreateAsync(user);

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

        if (await _orderService.CheckForActiveOrdersByUserIdAsync(id)) 
        {
            throw new RemoveErrorException(typeof(UserEntity), typeof(OrderEntity));
        }

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
