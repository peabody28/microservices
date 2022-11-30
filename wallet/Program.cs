using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy;
using Nancy.Owin;
using wallet;
using wallet.Entities;
using wallet.Interfaces.Entities;
using wallet.Interfaces.Operations;
using wallet.Interfaces.Repositories;
using wallet.Operations;
using wallet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// If using Kestrel:  
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:  
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IUser, UserEntity>();
builder.Services.AddTransient<IWallet, WalletEntity>();

builder.Services.AddSingleton<WalletDbContext, WalletDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

builder.Services.AddScoped<IWalletOperation, WalletOperation>();

var app = builder.Build();

app.UseOwin(func => func.UseNancy(options =>
{
    options.Bootstrapper = new Bootstrapper(app.Services);
}));

app.Run();