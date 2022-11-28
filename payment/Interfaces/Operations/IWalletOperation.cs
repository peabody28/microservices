using payment.Interfaces.Entities;

namespace payment.Interfaces.Operations
{
    public interface IWalletOperation
    {
        Task<IWallet> Create(string walletNumber);
    }
}
