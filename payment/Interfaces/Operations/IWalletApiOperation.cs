namespace payment.Interfaces.Operations
{
    public interface IWalletApiOperation
    {
        Task<bool> IsWalletExist(string walletNumber);
    }
}
