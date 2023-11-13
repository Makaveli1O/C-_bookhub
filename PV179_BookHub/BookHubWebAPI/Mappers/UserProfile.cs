using AutoMapper;
using BookHubWebAPI.Api.User.Create;
using BookHubWebAPI.Api.User.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User,  GeneralUserViewDto>();
        }
    }
}
