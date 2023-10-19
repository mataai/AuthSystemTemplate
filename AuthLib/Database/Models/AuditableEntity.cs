
using Authlib.Database.Models;

namespace AuthLib.Database.Models
{
    public abstract class AuditableEntity : ISoftDeletableEntity
    {

        public Guid Id { get; set; }
        public long CreatedTimestampUtc { get; set; }
        public long? UpdatedTimestampUtc { get; set; }
        public User CreatedByUser { get; set; }
        public User UpdatedByUser { get; set; }

        public bool IsDeleted { get; set; }
        public long? DeletedTimestampUtc { get; set; }
        public User? DeletedByUser { get; set; }
    }
}
