using Nancy;
using Nancy.Configuration;
using Nancy.TinyIoc;
using user.Repositories;

namespace user
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
            container.Register(_serviceProvider.GetRequiredService<UserDbContext>());
        }
    }
}
