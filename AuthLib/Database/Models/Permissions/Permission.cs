using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthLib.Database.Models.Permissions
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string System { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public Permission() { }
        public Permission(string system, string controller, string action, string description)
        {
            System = system;
            Controller = controller;
            Action = action;
            Description = description;
        }
    }
}
