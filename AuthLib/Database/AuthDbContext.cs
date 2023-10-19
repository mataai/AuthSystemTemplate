using Authlib.Database.Models;
using Authlib.Database.Models.Permissions;
using AuthLib.Database.Models;
using AuthLib.Database.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthLib.Database
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            //var connectionString = $"server={host};database={dbName};user={user};password={password}";
            var connectionString = $"server=127.0.0.1;database=Templates;user=user;password=P@ssw0rd";

            optionsBuilder.UseMySQL(connectionString, builder => builder.MigrationsAssembly("UserManagmentSystem"));
        }
        public void EnsureAuditTimestamp()
        {
            IEnumerable<EntityEntry> markedAsAuditable = ChangeTracker.Entries().Where(x => x.Entity is AuditableEntity);
            long unixTimeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var item in markedAsAuditable)
            {
                if (item.Entity is AuditableEntity entity)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entity.CreatedTimestampUtc = DateTime.UtcNow.Millisecond;
                                entity.UpdatedTimestampUtc = DateTime.UtcNow.Millisecond;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entity.UpdatedTimestampUtc = DateTime.UtcNow.Millisecond;
                                break;
                            }
                    }
                }
            }
        }
        public void EnsureSoftDelete()
        {
            IEnumerable<EntityEntry> markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            long unixTimeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (EntityEntry item in markedAsDeleted)
            {
                if (item.Entity is ISoftDeletableEntity entity)
                {
                    item.State = EntityState.Unchanged;
                    entity.IsDeleted = true;
                    entity.DeletedTimestampUtc = unixTimeNow;
                }
            }
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            EnsureSoftDelete();
            EnsureAuditTimestamp();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            ChangeTracker.DetectChanges();
            EnsureSoftDelete();
            EnsureAuditTimestamp();
            return await base.SaveChangesAsync();
        }

    }



}


