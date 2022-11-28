using user.Models.DTO.Wallet;

namespace user.Interfaces.Operations
{
    public interface IWalletApiOperation
    {
        public Task<WalletModel> Create(string username);
    }
}
