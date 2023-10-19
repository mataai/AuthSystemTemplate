    using AuthLib.Database.Models.Permissions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authlib.Database.Models.Permissions
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public Role() { }
        public Role(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

    }
}
