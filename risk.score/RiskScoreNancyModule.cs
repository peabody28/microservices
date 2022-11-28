using Nancy;
using Nancy.ModelBinding;
using risk.score.Interfaces.Repositories;
using risk.score.Models;

namespace risk.score
{
    public class RiskScoreNancyModule : NancyModule
    {
        public RiskScoreNancyModule(IRiskScoreRepository riskScoreRepository) : base("/risk.score")
        {
            Get("/get", async _ =>
            {
                var model = this.Bind<UserModel>();

                var userStatus = await riskScoreRepository.Object(model.Name);
                if (userStatus == null)
                    return Response.AsJson(new ErrorModel { Error = "USER_STATUS_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(new UserRiskScoreStatusModel { Username = model.Name, Status = userStatus.Status });
            });
        }
    }
}
