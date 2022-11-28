using Microsoft.EntityFrameworkCore;
using user.Entities;

namespace user.Repositories
{
    public class UserDbContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public UserDbContext(IConfiguration config)
        {
            Configuration = config;
        }

        public DbSet<UserEntity> User { get; set; }

        public DbSet<RoleEntity> Role { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("User");
            optionsBuilder.UseSqlite(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
               .HasOne(c => c.Role as RoleEntity);
        }
    }
}
