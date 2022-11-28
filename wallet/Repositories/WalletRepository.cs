using Microsoft.EntityFrameworkCore;
using wallet.Entities;
using wallet.Interfaces.Entities;
using wallet.Interfaces.Repositories;

namespace wallet.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        public WalletDbContext DbContext { get; set; }

        public IServiceProvider Container { get; set; }

        public WalletRepository(WalletDbContext walletDbContext, IServiceProvider container)
        {
            DbContext = walletDbContext;
            Container = container;
        }

        public async Task<IWallet> Create(IUser user, string number)
        {
            var entity = Container.GetRequiredService<IWallet>();
            entity.Id = Guid.NewGuid();
            entity.User = user;
            entity.Number = number;

            var wallet = await DbContext.Wallet.AddAsync(entity as WalletEntity);
            DbContext.Entry(entity.User).State = EntityState.Unchanged;
            await DbContext.SaveChangesAsync();

            return wallet.Entity;
        }

        public async Task<IWallet> Object(string username)
        {
            return await DbContext.Wallet.Include(wallet => wallet.User).AsAsyncEnumerable().FirstOrDefaultAsync(wallet => wallet.User.Name.Equals(username));
        }

        public async Task<IWallet> ObjectByNumber(string number)
        {
            return await DbContext.Wallet.AsAsyncEnumerable().FirstOrDefaultAsync(wallet => wallet.Number.Equals(number));
        }
    }
}
