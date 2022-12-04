using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Moq;
using Nancy;
using payment.Common;
using payment.Entities;
using payment.Interfaces.Entities;
using payment.Interfaces.Operations;
using payment.Operations;
using payment.Repositories;
using RichardSzalay.MockHttp;

namespace tests.payment
{
    public class Payment
    {
        private IConfiguration Configuration { get; set; }  

        private IWalletOperation WalletOperation { get; set; }

        private MockHttpMessageHandler MockHttpMessageHandler { get; set; }

        private const string authHeaderExample = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoia2F0aWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJEZWZhdWx0IiwibmJmIjoxNjY5ODM5Mjg1LCJleHAiOjE2Njk4Mzk4ODUsImlzcyI6ImF1dGgiLCJhdWQiOiJtaWNyb3NlcnZpY2VzIn0.5H6vQRpNoDMNWPwIkVHjX5mIghwl1x-OmWCxDF5lUvM";
        
        [SetUp]
        public void Setup()
        {
            MockHttpMessageHandler = new MockHttpMessageHandler();

            Configuration = new ConfigurationBuilder().AddJsonFile("E:\\work\\sharp\\microservices\\payment\\appsettings.json").Build();
            Configuration.GetSection("ConnectionStrings:Payment").Value = "Filename=E:/work/sharp/microservices/payment/db/payment.db";

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IWallet)))
                .Returns(new WalletEntity());

            var dbContext = new PaymentDbContext(Configuration);
            var walletRepository = new WalletRepository(dbContext, serviceProvider.Object);

            var client = new HttpClient(MockHttpMessageHandler);
            var nancyContext = new NancyContext() { Request = new Request("GET", string.Empty, HttpScheme.Http.ToString()) };
            nancyContext.Request.Headers.Authorization = authHeaderExample;

            var requestOperation = new RequestOperation(client, new CurrentRequest(nancyContext));
            var walletApiOperation = new WalletApiOperation(requestOperation, Configuration);
            WalletOperation = new WalletOperation(walletApiOperation, walletRepository);
        }

        [Test]
        public void CheckWallet([Values("WALLET")] string wallet, [Values(true, false)] bool isExists)
        {
            // register mock for IsExists Http request

            var url = Configuration.GetSection("Service:Wallet:Method:IsExist").Value;
            var data = new Dictionary<string, string> { { "number", wallet } };
            var uri = new Uri(QueryHelpers.AddQueryString(url, data));

            MockHttpMessageHandler.When(uri.ToString())
                    .Respond(req => new HttpResponseMessage(isExists ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest));

            // end registration

            var entity = WalletOperation.Get(wallet).Result;
            if (isExists)
                Assert.NotNull(entity);
            else
                Assert.Null(entity);
        }
    }
}