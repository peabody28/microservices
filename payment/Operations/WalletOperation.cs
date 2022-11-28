﻿using payment.Interfaces.Entities;
using payment.Interfaces.Operations;
using payment.Interfaces.Repositories;

namespace payment.Operations
{
    public class WalletOperation : IWalletOperation
    {
        public IWalletApiOperation WalletApiOperation { get; set; }

        public IWalletRepository WalletRepository { get; set; }

        public WalletOperation(IWalletApiOperation walletApiOperation, IWalletRepository walletRepository)
        {
            WalletRepository = walletRepository;
            WalletApiOperation = walletApiOperation;
        }

        public async Task<IWallet> Create(string walletNumber)
        {
            var isWalletExist = await WalletApiOperation.IsWalletExist(walletNumber);
            if (!isWalletExist)
                return null;

            return await WalletRepository.Create(walletNumber);
        }
    }
}
