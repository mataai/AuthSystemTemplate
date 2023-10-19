using Authlib.Database.Models.Permissions;
using AuthLib.Database.Models;
using AuthLib.Database.Models.Permissions;
using AuthLib.DataContracts.Operations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Authlib.Database.Models
{

    [Index(nameof(EmailAddress), IsUnique = true), Index(nameof(Id), IsUnique = true)]
    public class User 
    {
        [Key]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryLanguageCode { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public User() { }

        public User(UserCreateDto userCreateDto)
        {
            Id = userCreateDto.Id;
            FirstName = userCreateDto.FirstName;
            LastName = userCreateDto.LastName;
            EmailAddress = userCreateDto.EmailAddress;
            PrimaryLanguageCode = userCreateDto.PrimaryLanguageCode;
        }
    }
}
