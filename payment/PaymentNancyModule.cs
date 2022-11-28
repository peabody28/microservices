using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using payment.Interfaces.Operations;
using payment.Interfaces.Repositories;
using payment.Models;

namespace payment.NancyModels
{
    public class PaymentNancyModule : NancyModule
    {
        public PaymentNancyModule(IWalletRepository walletRepository, IWalletOperation walletOperation,
            IBalanceOperationTypeRepository balanceOperationTypeRepository, IPaymentRepository paymentRepository) : base("/payment")
        {
            Post("/create", async _ =>
            {
                this.RequiresAuthentication();

                var model = this.Bind<PaymentModel>();

                var wallet = await walletRepository.Object(model.WalletNumber) ?? await walletOperation.Create(model.WalletNumber);
                if(wallet == null)
                    return Response.AsJson(new ErrorModel { Error = "WALLET_NOT_FOUND" }, HttpStatusCode.BadRequest);

                var balanceOperationType = await balanceOperationTypeRepository.Object(model.BalanceOperationTypeCode);
                if (balanceOperationType == null)
                    return Response.AsJson(new ErrorModel { Error = "BALANCE_OPERATION_TYPE_NOT_FOUND" }, HttpStatusCode.BadRequest);

                var payment = await paymentRepository.Create(wallet, model.Amount, balanceOperationType);

                return Response.AsJson(new PaymentResponseModel { PaymentId = payment.Id });
            });
        }
    }
}
