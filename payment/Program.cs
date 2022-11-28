using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;
using payment;
using payment.Entities;
using payment.Interfaces.Entities;
using payment.Interfaces.Operations;
using payment.Interfaces.Repositories;
using payment.Operations;
using payment.Repositories;

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

builder.Services.AddTransient<IWallet, WalletEntity>();
builder.Services.AddTransient<IBalanceOperationType, BalanceOperationTypeEntity>();
builder.Services.AddTransient<IPayment, PaymentEntity>();

builder.Services.AddSingleton<PaymentDbContext, PaymentDbContext>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IBalanceOperationTypeRepository, BalanceOperationTypeRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddHttpClient<IRequestOperation, RequestOperation>();
builder.Services.AddScoped<IWalletOperation, WalletOperation>();
builder.Services.AddScoped<IWalletApiOperation, WalletApiOperation>();

var app = builder.Build();

app.UseOwin(func => func.UseNancy(options =>
{
    options.Bootstrapper = new Bootstrapper(app.Services);
}));

app.Run();