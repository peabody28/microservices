using auth.Models.DTO.User;

namespace auth.Interfaces.Operations
{
    public interface IUserApiOperation
    {
        Task<UserModel> GetUser(string username, string password);
    }
}
