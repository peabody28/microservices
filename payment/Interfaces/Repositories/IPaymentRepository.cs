using payment.Interfaces.Entities;

namespace payment.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<IPayment> Create(IWallet wallet, decimal amount, IBalanceOperationType balanceOperationType);
    }
}
