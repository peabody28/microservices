using Microsoft.EntityFrameworkCore;
using risk.score.Interfaces.Entities;
using risk.score.Interfaces.Repositories;

namespace risk.score.Repositories
{
    public class RiskScoreRepository : IRiskScoreRepository
    {
        private RiskScoreDbContext DbContext { get; set; }

        public RiskScoreRepository(RiskScoreDbContext riskScoreDbContext)
        {
            DbContext = riskScoreDbContext;
        }

        public async Task<IUserStatus> Object(string name)
        {
            return await DbContext.UserStatus.Include(status => status.User).AsAsyncEnumerable().FirstOrDefaultAsync(status => status.User.Name.Equals(name));
        }
    }
}
