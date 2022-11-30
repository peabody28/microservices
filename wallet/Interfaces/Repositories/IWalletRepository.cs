using wallet.Interfaces.Entities;

namespace wallet.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<IWallet> Create(IUser user, string number);

        Task<IEnumerable<IWallet>> Collection(IUser user);

        Task<IWallet> Object(IUser user, string number);
    }
}