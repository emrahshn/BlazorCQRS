using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Services
{
    public interface IStroopwafelsApiService
    {
        Task<IList<Stroopwafel>> GetProducts(string endpoint);
        Task<string> PostOrder<T>(string endpoint, T model);
    }
}