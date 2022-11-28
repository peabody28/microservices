using wallet.Interfaces.Entities;

namespace wallet.Interfaces.Operations
{
    public interface IWalletOperation
    {
        Task<IWallet> Create(string username);
    }
}
