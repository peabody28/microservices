using wallet.Interfaces.Entities;

namespace wallet.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<IWallet> Create(IUser user, string number);

        Task<IWallet> Object(string name);

        Task<IWallet> ObjectByNumber(string number);
    }
}