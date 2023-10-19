
namespace AuthLib.DataContracts
{
    public class RoleDto
    {
        public RoleDto(Guid id, string name, string description, IEnumerable<PermissionDto> permissions)
        {
            Id = id;
            Name = name;
            Description = description;
            Permissions = permissions;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<PermissionDto> Permissions { get; set; }

    }
}
