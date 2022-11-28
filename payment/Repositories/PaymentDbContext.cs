using Microsoft.EntityFrameworkCore;
using payment.Entities;

namespace payment.Repositories
{
    public class PaymentDbContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public PaymentDbContext(IConfiguration config)
        {
            Configuration = config;
        }


        public DbSet<BalanceOperationTypeEntity> BalanceOperationType { get; set; }

        public DbSet<WalletEntity> Wallet { get; set; }

        public DbSet<PaymentEntity> Payment { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("Payment");
            optionsBuilder.UseSqlite(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(c => c.Wallet as WalletEntity);

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(c => c.BalanceOperationType as BalanceOperationTypeEntity);
        }
    }
}
