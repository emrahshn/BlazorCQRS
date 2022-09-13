using Castle.Core.Logging;
using Microsoft.Extensions.Options;
using Stroopwafels.Shared.Helpers;
using Stroopwafels.Shared.Models;
using System.Text;

namespace Application.Services
{
    public abstract class BaseService
    {
        private readonly List<KeyValueModel> _apiHeader;
        private static readonly ILogger Logger = NullLogger.Instance;
        private readonly ServiceInformation _serviceInfo;
        public BaseService(IOptions<ServiceInformation> serviceInfo)
        {
            _serviceInfo = serviceInfo.Value;
            _apiHeader = new List<KeyValueModel> { new KeyValueModel { Property = "x-authorization", Value = _serviceInfo.BaseUrl } };
        }
        protected async Task<T> GetDataToApi<T>(string url)
        {
            try
            {
                return await HttpClientHelper.DeserializeFromStreamCall<T>(_serviceInfo.BaseUrl + url, HttpMethod.Get, null, _apiHeader).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.Error("StroopwafelsService." + url, e);
                throw;
            }
        }

        protected async Task<T> PostDataToApi<T>(string url, string jsonContent)
        {
            try
            {
                using StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                return await HttpClientHelper.DeserializeFromStreamCall<T>(_serviceInfo.BaseUrl + url, HttpMethod.Post, content, _apiHeader).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.Error("StroopwafelsService." + url, e);
                throw;
            }
        }
    }
}
