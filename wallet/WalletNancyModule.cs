using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System.Security.Claims;
using wallet.Interfaces.Entities;
using wallet.Interfaces.Operations;
using wallet.Interfaces.Repositories;
using wallet.Models;

namespace wallet
{
    public class WalletNancyModule : NancyModule
    {
        public IUserRepository UserRepository { get; set; }

        public IUser CurrentUser { 
            get
            {
                var name = Context.CurrentUser.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
                return UserRepository.Object(name).Result;
            }
        }

        public WalletNancyModule(IWalletOperation walletOperation, IWalletRepository walletRepository, IUserRepository userRepository) : base("/wallet")
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

                var wallet = await walletRepository.Object(CurrentUser.Name);
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
