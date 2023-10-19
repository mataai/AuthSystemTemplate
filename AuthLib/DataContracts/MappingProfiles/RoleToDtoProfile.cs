using Authlib.Database.Models.Permissions;
using AutoMapper;

namespace AuthLib.DataContracts.MappingProfiles
{
    public class RoleToDtoProfile : Profile
    {
        public RoleToDtoProfile()
        {
            CreateMap<Role, RoleDto>().ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));
            CreateMap<RoleDto, Role>();
        }
    }
}
