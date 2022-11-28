namespace payment.Interfaces.Entities
{
    public interface IPayment
    {
        Guid Id { get; set; }

        Guid WalletFk { get; set; }
        IWallet Wallet { get; set; }

        Guid BalanceOperationTypeFk { get; set; }
        IBalanceOperationType BalanceOperationType { get; set; }

        decimal Amount { get; set; }

        DateTime Created { get; set; }
    }
}
