using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Entities;

namespace UserService.Configurations
{
    public class UserDatabaseContext : IdentityDbContext<User>
    {      
            public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
                builder.Entity<User>().HasMany(field => field.Orders).WithOne().HasForeignKey(fk => fk.userId);
            }
            public DbSet<OrderInfo> Orders { get; set; }
        }
    
}
