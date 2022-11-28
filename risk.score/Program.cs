using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;
using risk.score;
using risk.score.Entities;
using risk.score.Interfaces.Entities;
using risk.score.Interfaces.Repositories;
using risk.score.Repositories;

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

builder.Services.AddTransient<IUser, UserEntity>();
builder.Services.AddTransient<IUserStatus, UserStatusEntity>();

builder.Services.AddSingleton<RiskScoreDbContext, RiskScoreDbContext>();
builder.Services.AddScoped<IRiskScoreRepository, RiskScoreRepository>();

var app = builder.Build();

app.UseOwin(func => func.UseNancy(options =>
{
    options.Bootstrapper = new Bootstrapper(app.Services);
}));

app.Run();
