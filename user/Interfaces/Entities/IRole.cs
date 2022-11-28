namespace user.Interfaces.Entities
{
    public interface IRole
    {
        Guid Id { get; set; }

        string Code { get; set; }
    }
}
