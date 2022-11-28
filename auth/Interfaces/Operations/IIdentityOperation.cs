using System.Security.Claims;

namespace auth.Interfaces.Operations
{
    public interface IIdentityOperation
    {
        Task<ClaimsIdentity> Object(string username, string password);
    }
}
