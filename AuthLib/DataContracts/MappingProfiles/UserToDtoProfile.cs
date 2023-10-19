using Authlib.Database.Models;
using AuthLib.DataContracts;
using AutoMapper;

namespace Core.MappingProfiles.Users
{
    public class UserToDtoProfile : Profile
    {
        public UserToDtoProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
            CreateMap<UserDto, User>();
        }
    }
}
