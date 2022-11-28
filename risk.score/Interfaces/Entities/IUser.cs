namespace risk.score.Interfaces.Entities
{
    public interface IUser
    {
        Guid Id { get; set; }

        string Name { get; set; }
    }
}
