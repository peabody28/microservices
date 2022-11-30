using wallet.Constants;
using wallet.Helpers;
using wallet.Interfaces.Entities;
using wallet.Interfaces.Operations;
using wallet.Interfaces.Repositories;

namespace wallet.Operations
{
    public class WalletOperation : IWalletOperation
    {

        public IUserRepository UserRepository { get; set; }

        public IWalletRepository WalletRepository { get; set; }

        public IServiceProvider Container { get; set; }

        public WalletOperation(IUserRepository userRepository, IWalletRepository walletRepository, IServiceProvider container)
        {
            UserRepository = userRepository;
            WalletRepository = walletRepository;
            Container = container;
        }

        public async Task<IWallet> Create(string username)
        {
            return await Container.InTransaction(() =>
            {
                var user = UserRepository.Object(username).Result ?? UserRepository.Create(username).Result;

                var number = GenerateNumber();
                var wallet = WalletRepository.Create(user, number).Result;

                return wallet;
            });
        }

        private string GenerateNumber()
        {
            var random = new Random();

            return new string(Enumerable.Repeat(CommonConstants.WalletNumberCharacters, CommonConstants.WalletNumberLength).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
