using risk.score.Interfaces.Entities;

namespace risk.score.Interfaces.Repositories
{
    public interface IRiskScoreRepository
    {
        Task<IUserStatus> Object(string name);
    }
}
