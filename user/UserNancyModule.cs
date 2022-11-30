using Nancy;
using Nancy.ModelBinding;
using user.Interfaces.Operations;
using user.Interfaces.Repositories;
using user.Models;

namespace user
{
    public class UserNancyModule : NancyModule
    {
        public UserNancyModule(IRiskScoreApiOperation riskScoreApiOperation, IUserRepository userRepository, IRoleRepository roleRepository) : base("/user")
        {
            Post("/register", async _ =>
            {
                var model = this.Bind<UserModel>();

                var riskScoreStatus = await riskScoreApiOperation.Status(model.Name);
                if(!riskScoreStatus)
                   return Response.AsJson(new ErrorModel { Error = "CANNOT_REGISTRATE_THIS_USER" }, HttpStatusCode.BadRequest);

                var existingEntity = await userRepository.Object(model.Name);
                if(existingEntity != null)
                    return Response.AsJson(new ErrorModel { Error = "USER_ALREADY_EXISTS" }, HttpStatusCode.BadRequest);

                var role = await roleRepository.Object("Default");
                var user = await userRepository.Create(model.Name, model.Password, role);

                return Response.AsJson(new UserRegistrationModel { Name = model.Name });
            });

            Post("/login", async _ =>
            {
                var model = this.Bind<UserModel>();

                var user = await userRepository.Object(model.Name, model.Password);
                if (user == null)
                    return Response.AsJson(new ErrorModel { Error = "USER_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(new UserInfoModel { Name = model.Name, Password = model.Password, RoleCode = user.Role.Code });
            });
        }
    }
}
