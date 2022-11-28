namespace wallet.Interfaces.Entities
{
    public interface IWallet
    {
        Guid Id { get; set; }
        Guid UserFk { get; set; }
        IUser User { get; set; }
        string Number { get; set; }
    }
}
