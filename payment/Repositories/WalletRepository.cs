using payment.Entities;
using payment.Interfaces.Entities;
using payment.Interfaces.Repositories;

namespace payment.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        public IServiceProvider Container { get; set; }

        public PaymentDbContext DbContext { get; set; }

        public WalletRepository(PaymentDbContext paymentDbContext, IServiceProvider container)
        {
            Container = container;
            DbContext = paymentDbContext;
        }

        public async Task<IWallet> Create(string walletNumber)
        {
            var entity = Container.GetRequiredService<IWallet>();
            entity.Id = Guid.NewGuid();
            entity.Number = walletNumber;

            var wallet = await DbContext.Wallet.AddAsync(entity as WalletEntity);
            await DbContext.SaveChangesAsync();
            return wallet.Entity;
        }

        public async Task<IWallet> Object(string walletNumber)
        {
            return await DbContext.Wallet.AsAsyncEnumerable().FirstOrDefaultAsync(wallet => wallet.Number.Equals(walletNumber));
        }
    }
}
