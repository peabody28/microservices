using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using wallet.Interfaces.Operations;
using wallet.Interfaces.Repositories;
using wallet.Models;

namespace wallet
{
    public class WalletNancyModule : NancyModule
    {
        public WalletNancyModule(IWalletOperation walletOperation, IWalletRepository walletRepository) : base("/wallet")
        {
            Post("/create", async _ =>
            {
                var model = this.Bind<UserModel>();

                var existingEntity = await walletRepository.Object(model.Name);
                if (existingEntity != null)
                    return Response.AsJson(new ErrorModel { Error = "WALLET_ALREADY_EXIST" }, HttpStatusCode.BadRequest);

                var wallet = await walletOperation.Create(model.Name);

                return Response.AsJson(new WalletModel { Username = wallet.User.Name, Number = wallet.Number });
            });

            Get("/get", async _ =>
            {
                this.RequiresAuthentication();

                var model = this.Bind<UserModel>();

                var wallet = await walletRepository.Object(model.Name);
                if(wallet == null)
                    return Response.AsJson(new ErrorModel { Error = "WALLET_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(new WalletModel { Username = wallet.User.Name, Number = wallet.Number });
            });

            Get("/isExist", async _ =>
            {
                this.RequiresAuthentication();

                var model = this.Bind<WalletNumberModel>();

                var wallet = await walletRepository.ObjectByNumber(model.Number);
                if (wallet == null)
                    return Response.AsJson(new ErrorModel { Error = "WALLET_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(model);
            });
        }
    }
}
