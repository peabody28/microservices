using user.Interfaces.Entities;

namespace user.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IRole> Object(string code);
    }
}
