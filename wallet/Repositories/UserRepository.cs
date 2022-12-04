using wallet.Entities;
using wallet.Interfaces.Entities;
using wallet.Interfaces.Repositories;

namespace wallet.Repositories
{
    public class UserRepository : IUserRepository
    {
        public WalletDbContext DbContext { get; set; }

        public IServiceProvider Container { get; set; }

        public UserRepository(WalletDbContext userDbContext, IServiceProvider container)
        {
            DbContext = userDbContext;
            Container = container;
        }

        public async Task<IUser> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var entity = Container.GetRequiredService<IUser>();
            entity.Id = Guid.NewGuid();
            entity.Name = name;

            var user = await DbContext.User.AddAsync(entity as UserEntity);
            await DbContext.SaveChangesAsync();

            return user.Entity;
        }

        public async Task<IUser> Object(string name)
        {
            return await DbContext.User.AsAsyncEnumerable().FirstOrDefaultAsync(user => user.Name.Equals(name));
        }
    }
}
