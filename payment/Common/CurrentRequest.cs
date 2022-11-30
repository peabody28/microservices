using Nancy;
using payment.Interfaces.Common;

namespace payment.Common
{
    public class CurrentRequest : ICurrentRequest
    {
        public CurrentRequest(NancyContext context)
        {
            this.Context = context;
        }

        public NancyContext Context { get; set; }
    }
}
