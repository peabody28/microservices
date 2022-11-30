using Nancy;

namespace payment.Interfaces.Common
{
    public interface ICurrentRequest
    {
        NancyContext Context { get; set; }
    }
}
