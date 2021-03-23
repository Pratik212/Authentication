using Authentication.Dtos;
using Authentication.Models;
using AutoMapper;

namespace Authentication.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}