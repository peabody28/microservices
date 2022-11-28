using payment.Interfaces.Entities;

namespace payment.Interfaces.Repositories
{
    public interface IBalanceOperationTypeRepository
    {
        Task<IBalanceOperationType> Object(string balanceOperationTypeCode);
    }
}
