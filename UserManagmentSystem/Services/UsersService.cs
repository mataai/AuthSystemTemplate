using Authlib.Database.Models;
using AuthLib.Database;
using AuthLib.DataContracts;
using AuthLib.DataContracts.Operations;
using AuthLib.DataContracts.ReponseUtils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UserManagmentSystem.Services
{
    public class UsersService 
    {

        protected IMapper mapper;
        protected readonly AuthDbContext context;

        public UsersService(AuthDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            return this.mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            User? user = await context.Users.FindAsync(id);
            if (user == null)
            {
                throw new CustomErrorException(HttpStatusCode.NotFound, "user-not-found", "User dosen't exist, is probably not signed up.", "");
            }
            return mapper.Map<UserDto>(user);
        }

        public async Task<AuthResponse<UserDto>> CreateUserAsync(UserCreateDto createUserDto)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.EmailAddress == createUserDto.EmailAddress);

            if (existingUser != null)
            {
                throw new CustomErrorException(HttpStatusCode.BadRequest, "user-exist", "User already exists with index information", "");
            };


            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                EmailAddress = createUserDto.EmailAddress,
                Id = createUserDto.Id,
                PrimaryLanguageCode = string.IsNullOrWhiteSpace(createUserDto.PrimaryLanguageCode) ? "en-US" : createUserDto.PrimaryLanguageCode,
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new AuthResponse<UserDto>(mapper.Map<UserDto>(user));
        }

        public async Task<UserDto?> UpdateUserAsync(string id, UserUpdateDto updateUserDto)
        {
            User? user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.PrimaryLanguageCode = updateUserDto.PrimaryLanguageCode;
            await context.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<AuthResponse> DeleteUserAsync(string id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                throw new CustomErrorException(HttpStatusCode.BadRequest, "user-not-found", "User dosen't exist", "");
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return new AuthResponse(true, "");
        }
    }
}
