using Microsoft.EntityFrameworkCore;
using user.Interfaces.Entities;
using user.Interfaces.Repositories;

namespace user.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public UserDbContext DbContext { get; set; }

        public IServiceProvider Container { get; set; }

        public RoleRepository(UserDbContext userDbContext, IServiceProvider container)
        {
            DbContext = userDbContext;
            Container = container;
        }

        public async Task<IRole> Object(string code)
        {
            return await DbContext.Role.AsAsyncEnumerable().FirstOrDefaultAsync(role => role.Code.Equals(code));
        }
    }
}
