using payment.Interfaces.Entities;
using payment.Interfaces.Repositories;

namespace payment.Repositories
{
    public class BalanceOperationTypeRepository : IBalanceOperationTypeRepository
    {

        public PaymentDbContext DbContext { get; set; }

        public BalanceOperationTypeRepository(PaymentDbContext paymentDbContext)
        {
            DbContext = paymentDbContext;
        }

        public async Task<IBalanceOperationType> Object(string balanceOperationTypeCode)
        {
            return await DbContext.BalanceOperationType.AsAsyncEnumerable().FirstOrDefaultAsync(type => type.Code.Equals(balanceOperationTypeCode));
        }
    }
}
