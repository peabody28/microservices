namespace payment.Interfaces.Entities
{
    public interface IBalanceOperationType
    {
        Guid Id { get; set; }

        string Code { get; set; }
    }
}
