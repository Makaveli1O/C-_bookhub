using AutoMapper;
using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Account;

namespace BusinessLayer.Mappers.Enitity
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<UserEntity, GeneralUserViewDto>();
            CreateMap<LocalIdentityUser, GeneralUserViewDto>()
                .ForMember(m => m.UserName, u => u.MapFrom(m => m.UserName))
                .ForMember(m => m.Id, u => u.MapFrom(m => m.User!.Id));
        }
    }
}
