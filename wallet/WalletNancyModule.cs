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
            UserRepository = userRepository;

            Post("/create", async _ =>
            {
                this.RequiresAuthentication();

                var username = Context.CurrentUser.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
                var wallet = await walletOperation.Create(username);

                return Response.AsJson(new WalletModel { Number = wallet.Number });
            });

            Get("/get", async _ =>
            {
                this.RequiresAuthentication();

                var wallets = await walletRepository.Collection(CurrentUser);
                if(wallets == null || !wallets.Any())
                    return Response.AsJson(new ErrorModel { Error = "WALLETS_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(wallets.Select(wallet => new WalletModel { Number = wallet.Number }));
            });

            Get("/isExist", async _ =>
            {
                this.RequiresAuthentication();

                var model = this.Bind<WalletNumberModel>();

                var wallet = await walletRepository.Object(CurrentUser, model.Number);
                if (wallet == null)
                    return Response.AsJson(new ErrorModel { Error = "WALLET_NOT_FOUND" }, HttpStatusCode.BadRequest);

                return Response.AsJson(model);
            });
        }
    }
}
