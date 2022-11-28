using System.Security.Claims;
using auth.Interfaces.Operations;

namespace auth.Operations
{
    public class IdentityOperation : IIdentityOperation
    {
        #region [ Dependency -> Repositories ]

        public IUserApiOperation UserApiOperation { get; set; }

        #endregion

        public IdentityOperation(IUserApiOperation  userApiOperation)
        {
            UserApiOperation= userApiOperation;
        }

        public async Task<ClaimsIdentity> Object(string username, string password)
        {
            var user = await UserApiOperation.GetUser(username, password);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleCode)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
