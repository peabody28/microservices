namespace payment.Interfaces.Operations
{
    public interface IRequestOperation
    {
        Task<bool> Get(string url, IDictionary<string, string> data);
    }
}
