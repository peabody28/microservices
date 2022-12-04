using Microsoft.Extensions.Configuration;
using risk.score.Interfaces.Repositories;
using risk.score.Repositories;

namespace tests.risk.score
{
    public class RiskScore
    {
        public IRiskScoreRepository RiskScoreRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("E:\\work\\sharp\\microservices\\risk.score\\appsettings.json").Build();
            config.GetSection("ConnectionStrings:RiskScore").Value = "Filename=E:/work/sharp/microservices/risk.score/db/risk.score.db";

            var dbContext = new RiskScoreDbContext(config);
            RiskScoreRepository = new RiskScoreRepository(dbContext); 
        }

        [Test]
        public async Task TestGet([Values("max", "none")]string name)
        {
            var resp = await RiskScoreRepository.Object(name);

            switch(name)
            {
                case "max":
                    Assert.NotNull(resp);
                    break;
                case "none":
                    Assert.Null(resp);
                    break;
            }
        }
    }
}