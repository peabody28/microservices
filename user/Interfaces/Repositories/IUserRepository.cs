using user.Interfaces.Entities;

namespace user.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> Object(string name);

        Task<IUser> Object(string name, string password);

        Task<IUser> Create(string name, string password, IRole role);
    }
}
