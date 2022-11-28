namespace payment.Interfaces.Entities
{
    public interface IWallet
    {
        Guid Id { get; set; }
        string Number { get; set; }
    }
}
