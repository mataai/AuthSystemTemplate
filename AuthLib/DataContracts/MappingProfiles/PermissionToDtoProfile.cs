using AuthLib.Database.Models.Permissions;
using AuthLib.DataContracts;
using AutoMapper;
namespace Core.MappingProfiles.Users
{
    public class PermissionToDtoProfile : Profile
    {
        public PermissionToDtoProfile()
        {
            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();
        }

    }
}
