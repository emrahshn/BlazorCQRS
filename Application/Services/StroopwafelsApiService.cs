using Microsoft.Extensions.Options;
using Stroopwafels.Shared.Helpers;
using Stroopwafels.Shared.Models;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Services
{
    public class StroopwafelsApiService: BaseService, IStroopwafelsApiService
    {
        public StroopwafelsApiService(IOptions<ServiceInformation> serviceInfo) : base(serviceInfo)
        {
        }

        public async Task<IList<Stroopwafel>> GetProducts(string endpoint)
        {
            var response = await GetDataToApi<IList<Stroopwafel>>(endpoint);
            return response;
        }
        public async Task<string> PostOrder<T>(string endpoint,T model)
        {
            var jsonContent = await HttpClientHelper.GetContent(model).ReadAsStringAsync();
            var response = await PostDataToApi<string>(endpoint, jsonContent);
            return response;
        }
    }
}
