using AuthLib.DataContracts;
using AuthLib.DataContracts.Operations;
using AuthLib.DataContracts.ReponseUtils;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Services;

namespace UserManagmentSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersService _userService;

        public UserController(UsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthResponse<UserDto> response = await _userService.CreateUserAsync(createUserDto);
            if (response.IsSuccess == true)
            {
                var user = response.Data;
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]

        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserById(string id, [FromBody] UserUpdateDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.UpdateUserAsync(id, updateUserDto);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

}