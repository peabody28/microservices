using wallet.Interfaces.Entities;

namespace wallet.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> Create(string name);

        Task<IUser> Object(string name);
    }
}
