using Microsoft.IdentityModel.Tokens;
using Nancy;
using Nancy.Authentication.JwtBearer;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using payment.Repositories;
using System.Text;

namespace payment
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        readonly IServiceProvider _serviceProvider;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(true, true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(_serviceProvider.GetRequiredService<IConfiguration>());
            container.Register(_serviceProvider.GetRequiredService<IServiceProvider>());
            container.Register(_serviceProvider.GetRequiredService<IHttpContextAccessor>());
            container.Register(_serviceProvider.GetRequiredService<PaymentDbContext>());
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("AuthOptions:ISSUER").Value,
                ValidateAudience = true,
                ValidAudience = configuration.GetSection("AuthOptions:AUDIENCE").Value,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("AuthOptions:KEY").Value)),
                ValidateIssuerSigningKey = true
            };

            var x = new JwtBearerAuthenticationConfiguration
            {
                TokenValidationParameters = tokenValidationParameters
            };

            pipelines.EnableJwtBearerAuthentication(x);
        }
    }
}
