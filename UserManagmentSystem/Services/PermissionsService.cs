using AuthLib.Database;
using AuthLib.Database.Models.Permissions;
using AuthLib.DataContracts;
using AuthLib.DataContracts.Operations;
using AuthLib.DataContracts.ReponseUtils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UserManagmentSystem.Services
{
    public class PermissionsService
    {

        protected IMapper mapper;
        protected readonly AuthDbContext context;

        public PermissionsService(AuthDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await context.Permissions.ToListAsync();
            return this.mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(string id)
        {
            Permission? permission = await context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new CustomErrorException(HttpStatusCode.NotFound, "permission-not-found", "Permission dosen't exist.", "");
            }
            return mapper.Map<PermissionDto>(permission);
        }

        public async Task<AuthResponse<PermissionDto>> CreatePermissionAsync(PermissionCreateDto createPermissionDto)
        {
            var existingPermission = await context.Permissions.FirstOrDefaultAsync(u => u.System == createPermissionDto.System && u.Controller == createPermissionDto.Controller && u.Action == createPermissionDto.Action);

            if (existingPermission != null)
            {
                throw new CustomErrorException(HttpStatusCode.BadRequest, "permission-exist", "Permission already exists with index information", "");
            };


            var permission = this.mapper.Map<Permission>(createPermissionDto);
            context.Permissions.Add(permission);
            await context.SaveChangesAsync();
            return new AuthResponse<PermissionDto>(mapper.Map<PermissionDto>(permission));
        }

        public async Task<PermissionDto?> UpdatePermissionAsync(string id, PermissionUpdateDto permissionUpdateDto)
        {
            Permission? permission = await context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new CustomErrorException(HttpStatusCode.BadRequest, "perm-not-found", "Permmission dosen't exist", "");
            }
            permission.Description = permissionUpdateDto.Description;
            permission.System = permissionUpdateDto.System;
            permission.Controller = permissionUpdateDto.Controller;
            permission.Action = permissionUpdateDto.Action;
            await context.SaveChangesAsync();
            return mapper.Map<PermissionDto>(permission);
        }

        public async Task<AuthResponse> DeletePermissionAsync(string id)
        {
            var perm = await context.Permissions.FindAsync(id);
            if (perm == null)
            {
                throw new CustomErrorException(HttpStatusCode.BadRequest, "perm-not-found", "Permmission dosen't exist", "");
            }
            context.Permissions.Remove(perm);
            await context.SaveChangesAsync();
            return new AuthResponse(true, "");
        }
    }
}
