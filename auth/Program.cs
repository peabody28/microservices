using auth;
using auth.Interfaces.Operations;
using auth.Operations;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;

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

builder.Services.AddScoped<JwtTokenOperation, JwtTokenOperation>();
builder.Services.AddHttpClient<IRequestOperation, RequestOperation>();
builder.Services.AddScoped<IIdentityOperation, IdentityOperation>();
builder.Services.AddScoped<IUserApiOperation, UserApiOperation>();


var app = builder.Build();

app.UseOwin(func => func.UseNancy(options =>
{
    options.Bootstrapper = new Bootstrapper(app.Services);
}));

app.Run();
