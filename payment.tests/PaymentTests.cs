using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Moq;
using Nancy;
using payment.Common;
using payment.Entities;
using payment.Interfaces.Entities;
using payment.Interfaces.Operations;
using payment.Interfaces.Repositories;
using payment.Operations;
using payment.Repositories;
using RichardSzalay.MockHttp;

namespace payment.tests
{
    public class PaymentTests
    {
        private IConfiguration Configuration { get; set; }

        private IWalletOperation WalletOperation { get; set; }

        private IPaymentRepository PaymentRepository { get; set; }

        private IBalanceOperationTypeRepository BalanceOperationTypeRepository { get; set; }

        private MockHttpMessageHandler MockHttpMessageHandler { get; set; }


        private const string authHeaderExample = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoia2F0aWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJEZWZhdWx0IiwibmJmIjoxNjY5ODM5Mjg1LCJleHAiOjE2Njk4Mzk4ODUsImlzcyI6ImF1dGgiLCJhdWQiOiJtaWNyb3NlcnZpY2VzIn0.5H6vQRpNoDMNWPwIkVHjX5mIghwl1x-OmWCxDF5lUvM";


        [SetUp]
        public void Setup()
        {
            MockHttpMessageHandler = new MockHttpMessageHandler();

            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(IWallet))).Returns(new WalletEntity());
            serviceProvider.Setup(x => x.GetService(typeof(IPayment))).Returns(new PaymentEntity());

            var dbContext = new PaymentDbContext(Configuration);
            var walletRepository = new WalletRepository(dbContext, serviceProvider.Object);
            BalanceOperationTypeRepository = new BalanceOperationTypeRepository(dbContext);
            PaymentRepository = new PaymentRepository(dbContext, serviceProvider.Object);

            var client = new HttpClient(MockHttpMessageHandler);

            var nancyContext = new NancyContext() { Request = new Request("GET", string.Empty, HttpScheme.Http.ToString()) };
            nancyContext.Request.Headers.Authorization = authHeaderExample;

            var requestOperation = new RequestOperation(client, new CurrentRequest(nancyContext));
            var walletApiOperation = new WalletApiOperation(requestOperation, Configuration);

            WalletOperation = new WalletOperation(walletApiOperation, walletRepository);
        }

        /// <summary>
        /// Test for WalletOperation.Get() method
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="isExistsInExternalService">if true, then wallet exist in some wallet service</param>
        [Test]
        public void GetWallet([Values("WALLET")] string walletNumber, [Values(true, false)] bool isExistsInExternalService)
        {
            var url = Configuration.GetSection("Service:Wallet:Method:IsExist").Value;
            MockHttpMessageHandler.When(url)
                    .Respond(req => new HttpResponseMessage(isExistsInExternalService ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest));

            var wallet = WalletOperation.Get(walletNumber).Result;

            if (isExistsInExternalService) 
            {
                Assert.NotNull(wallet);
                Assert.That(walletNumber, Is.EqualTo(wallet.Number));
            }
            else
                Assert.Null(wallet);
        }

        /// <summary>
        /// Test for PaymentRepository.Create() method
        /// </summary>
        /// <param name="walletNumber"></param>
        /// <param name="amount"></param>
        /// <param name="botCode"></param>
        [Test]
        public void CreatePayment([Values("WALLET", "", null)] string walletNumber, [Values(5)] decimal amount, [Values("Credit", "Debit", "None")] string botCode)
        {
            var url = Configuration.GetSection("Service:Wallet:Method:IsExist").Value;
            MockHttpMessageHandler.When(url).Respond(req => new HttpResponseMessage(System.Net.HttpStatusCode.OK));

            var wallet = WalletOperation.Get(walletNumber).Result;
            var balanceOperationType = BalanceOperationTypeRepository.Object(botCode).Result;
            var payment = PaymentRepository.Create(wallet, amount, balanceOperationType).Result;

            if (balanceOperationType == null || wallet == null)
                Assert.Null(payment);
            else
            {
                Assert.NotNull(payment);
                Assert.That(walletNumber, Is.EqualTo(payment.Wallet.Number));
                Assert.That(amount, Is.EqualTo(payment.Amount));
                Assert.That(botCode, Is.EqualTo(payment.BalanceOperationType.Code));
            }    
        }
    }
}