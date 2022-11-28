namespace risk.score.Interfaces.Entities
{
    public interface IUserStatus
    {
        Guid Id { get; set; }
        Guid UserFk { get; set; }
        IUser User { get; set; }
        bool Status { get; set; }
    }
}
