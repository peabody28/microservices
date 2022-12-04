using Microsoft.EntityFrameworkCore;
using payment.Entities;
using payment.Interfaces.Entities;
using payment.Interfaces.Repositories;

namespace payment.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentDbContext DbContext { get; set; }

        public IServiceProvider Container { get; set; }

        public PaymentRepository(PaymentDbContext paymentDbContext, IServiceProvider container)
        {
            DbContext = paymentDbContext;
            Container = container;
        }

        public async Task<IPayment> Create(IWallet wallet, decimal amount, IBalanceOperationType balanceOperationType)
        {
            if (wallet == null || balanceOperationType == null)
                return null;

            var entity = Container.GetRequiredService<IPayment>();
            entity.Id = Guid.NewGuid();
            entity.Wallet = wallet;
            entity.BalanceOperationType = balanceOperationType;
            entity.Amount = amount;
            entity.Created = DateTime.UtcNow;

            var payment = await DbContext.Payment.AddAsync(entity as PaymentEntity);
            DbContext.Entry(entity.Wallet).State = EntityState.Unchanged;
            DbContext.Entry(entity.BalanceOperationType).State = EntityState.Unchanged;
            await DbContext.SaveChangesAsync();

            return payment.Entity;
        }
    }
}
