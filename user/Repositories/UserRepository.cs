using Microsoft.EntityFrameworkCore;
using user.Entities;
using user.Helpers;
using user.Interfaces.Entities;
using user.Interfaces.Repositories;

namespace user.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserDbContext DbContext { get; set; }

        public IServiceProvider Container { get; set; }

        public UserRepository(UserDbContext userDbContext, IServiceProvider container)
        {
            DbContext = userDbContext;
            Container = container;
        }

        public async Task<IUser> Object(string name)
        {
            return await DbContext.User.AsAsyncEnumerable().FirstOrDefaultAsync(user => user.Name.Equals(name));
        }

        public async Task<IUser> Object(string name, string password)
        {
            var passwordHash = MD5Helper.Hash(password);
            return await DbContext.User.Include(user => user.Role).AsAsyncEnumerable().FirstOrDefaultAsync(user => user.Name.Equals(name) && user.PasswordHash.Equals(passwordHash));
        }

        public async Task<IUser> Create(string name, string password, IRole role)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || role == null)
                return null;

            var entity = Container.GetRequiredService<IUser>();
            entity.Id = Guid.NewGuid();
            entity.Name = name;
            entity.PasswordHash = MD5Helper.Hash(password);
            entity.Role = role;

            var user = await DbContext.User.AddAsync(entity as UserEntity);
            DbContext.Entry(entity.Role).State = EntityState.Unchanged;
            await DbContext.SaveChangesAsync();
            return user.Entity;
        }
    }
}
