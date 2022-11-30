using Nancy;
using Nancy.Bootstrapper;
using payment.Interfaces.Common;

namespace payment.Common
{
    public class PrepareRequestContext : IRequestStartup
    {
        private readonly ICurrentRequest Context;

        public PrepareRequestContext(ICurrentRequest nancyContext)
        {
            Context = nancyContext;
        }

        public void Initialize(IPipelines piepeLinse, NancyContext context)
        {
            Context.Context = context;
        }
    }
}
