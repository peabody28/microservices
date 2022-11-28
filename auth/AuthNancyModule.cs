using auth.Interfaces.Operations;
using auth.Models;
using auth.Operations;
using Nancy;
using Nancy.ModelBinding;

namespace auth
{
    public class AuthNancyModule : NancyModule
    {
        public AuthNancyModule(IIdentityOperation identityOperation, JwtTokenOperation jwtTokenOperation) : base("/auth")
        {
            Post("/token", async _ =>
            {
                var model = this.Bind<UserModel>();

                var identity = await identityOperation.Object(model.Name, model.Password);
                if (identity == null)
                    return Response.AsJson(new ErrorModel { Error = "USER_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(new TokenModel { AccessToken = jwtTokenOperation.Generate(identity), Username = identity.Name });
            });
        }
    }
}
