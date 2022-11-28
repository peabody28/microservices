using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;
using user;
using user.Entities;
using user.Interfaces.Entities;
using user.Interfaces.Operations;
using user.Interfaces.Repositories;
using user.Operations;
using user.Repositories;

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
builder.Services.AddTransient<IRole, RoleEntity>();


builder.Services.AddSingleton<UserDbContext, UserDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddHttpClient<IRequestOperation, RequestOperation>();
builder.Services.AddScoped<IRiskScoreApiOperation, RiskScoreApiOperation>();

var app = builder.Build();

app.UseOwin(func => func.UseNancy(options => 
{
    options.Bootstrapper = new Bootstrapper(app.Services);
}));

app.Run();
