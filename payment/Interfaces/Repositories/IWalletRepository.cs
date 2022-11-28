using payment.Interfaces.Entities;

namespace payment.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<IWallet> Object(string walletNumber);

        Task<IWallet> Create(string walletNumber);
    }
}
