using Authlib.Database.Models;
namespace AuthLib.Database.Models
{
    public interface ISoftDeletableEntity
    {
        public bool IsDeleted { get; set; }
        public long? DeletedTimestampUtc { get; set; }
        public User? DeletedByUser { get; set; }
    }
}
