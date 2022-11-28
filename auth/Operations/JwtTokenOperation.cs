using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth.Operations
{
    public class JwtTokenOperation
    {
        public IConfiguration Configuration { get; set; }

        public JwtTokenOperation(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Generate(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var issuer = Configuration.GetSection("AuthOptions:ISSUER").Value;
            var audience = Configuration.GetSection("AuthOptions:AUDIENCE").Value;
            var lifetime = Configuration.GetSection("AuthOptions:LIFETIME").Get<double>();
            var key = Configuration.GetSection("AuthOptions:KEY").Value;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var jwt = new JwtSecurityToken(issuer, audience, identity.Claims, now,
                now.Add(TimeSpan.FromMinutes(lifetime)), new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
