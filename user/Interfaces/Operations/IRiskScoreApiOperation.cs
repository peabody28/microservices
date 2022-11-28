namespace user.Interfaces.Operations
{
    public interface IRiskScoreApiOperation
    {
        Task<bool> Status(string username);
    }
}
