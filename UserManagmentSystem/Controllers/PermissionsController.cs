using AuthLib.DataContracts;
using AuthLib.DataContracts.Operations;
using AuthLib.DataContracts.ReponseUtils;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Services;

namespace PermissionManagmentSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionsService _permissionsService;

        public PermissionsController(PermissionsService permissionService)
        {
            _permissionsService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var perms = await _permissionsService.GetAllPermissionsAsync();
            return Ok(perms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(string id)
        {
            var perm = await _permissionsService.GetPermissionByIdAsync(id);
            return Ok(perm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionCreateDto createPermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthResponse<PermissionDto> response = await _permissionsService.CreatePermissionAsync(createPermissionDto);
            if (response.IsSuccess == true)
            {
                var permission = response.Data;
                return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permission);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePermissionById(string id, [FromBody] PermissionUpdateDto updatePermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var permission = await _permissionsService.UpdatePermissionAsync(id, updatePermissionDto);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(string id)
        {
            var result = await _permissionsService.DeletePermissionAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

}