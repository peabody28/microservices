using Moq;
using payment.Entities;
using payment.Interfaces.Operations;
using payment.Interfaces.Repositories;
using payment.Operations;

namespace payment.tests
{
    public class PaymentTests
    {
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Test for WalletOperation.Get() method
        /// <br>Guarantees if the wallet exists in the some external system it will be raised</br>
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="isExistsInExternalService">if true, then wallet exist in some wallet service</param>
        [Test]
        public void GetWallet([Values("WALLET")] string walletNumber, [Values(true, false)] bool isExistsInExternalService)
        {
            // arrange
            Mock<IWalletApiOperation> walletApiOperationMock = new Mock<IWalletApiOperation>();
            walletApiOperationMock.Setup(x => x.IsWalletExist(walletNumber).Result).Returns(isExistsInExternalService);

            Mock<IWalletRepository> walletRepositoryMock = new Mock<IWalletRepository>();
            walletRepositoryMock.Setup(x => x.Object(walletNumber).Result).Returns(new WalletEntity { Number = walletNumber });
            walletRepositoryMock.Setup(x => x.Create(walletNumber).Result).Returns(new WalletEntity { Number = walletNumber });

            var walletOperation = new WalletOperation(walletApiOperationMock.Object, walletRepositoryMock.Object);

            // act
            var wallet = walletOperation.Get(walletNumber).Result;

            // assert
            if (isExistsInExternalService)
            {
                Assert.NotNull(wallet);
                Assert.That(walletNumber, Is.EqualTo(wallet.Number));
            }
            else
                Assert.Null(wallet);
        }
    }
}