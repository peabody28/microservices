using Microsoft.EntityFrameworkCore;
using risk.score.Entities;

namespace risk.score.Repositories
{
    public class RiskScoreDbContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public RiskScoreDbContext(IConfiguration config)
        {
            Configuration = config;
        }

        public DbSet<UserEntity> User { get; set; }
       
        public DbSet<UserStatusEntity> UserStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("RiskScore");
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<UserStatusEntity>()
                .HasOne(c => c.User as UserEntity);
        }
    }
}
