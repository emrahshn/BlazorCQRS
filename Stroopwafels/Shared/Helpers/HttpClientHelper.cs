using Castle.Core.Logging;
using Newtonsoft.Json;
using Stroopwafels.Shared.Models;
using System.Net;
using System.Net.Http.Headers;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Stroopwafels.Shared.Helpers
{
    public static class HttpClientHelper
    {
        private static readonly HttpClientHandler ClientHandler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        private static readonly HttpClient Client = new HttpClient(ClientHandler);
        private static readonly ILogger Logger = NullLogger.Instance;
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);
        private const string MediaType = "application/json";
        public static async Task<T> DeserializeFromStreamCall<T>(string url, HttpMethod httpMethod, HttpContent content = null, IEnumerable<KeyValueModel> headers = null)
        {
            Logger.Info($"HttpClientHelper.DeserializeFromStreamCall: {url} {content}");
            using HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            CreateRequestHeaders(request, headers);
            request.Content = content;
            //send request
            using HttpResponseMessage responseMessage = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, GetCancellationTokenSource());
            //read as stream
            content = responseMessage.Content;
            Stream response = await content.ReadAsStreamAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return DeserializeJsonFromStream<T>(response);
                }
                catch (Exception e)
                {
                    Logger.Error("HttpClientHelper.DeserializeFromStreamCall", e);
                }
            }

            //http status code != 200
            try
            {
                using StreamReader sr = new StreamReader(response);
                string contentResult = await sr.ReadToEndAsync();
                Logger.Error("HttpClientHelper.DeserializeFromStreamCall", new Exception($"StatusCode:{responseMessage.StatusCode}, Content:{contentResult}"));
            }
            catch (Exception e)
            {
                Logger.Error("HttpClientHelper.DeserializeFromStreamCall", e);
            }

            return default;
        }

        public static async Task<string> GetStringResult(string url, HttpMethod httpMethod, HttpContent content = null, IEnumerable<KeyValueModel> headers = null)
        {
            Logger.Info($"HttpClientHelper.GetStringResult: {url}");
            using HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            CreateRequestHeaders(request, headers);
            request.Content = content;
            //send request
            using HttpResponseMessage responseMessage = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, GetCancellationTokenSource());
            //read as stream
            string response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return response;
            }

            //http status code != 200
            Logger.Error("HttpClientHelper.GetStringResult", new Exception($"StatusCode:{responseMessage.StatusCode}"));
            return null;
        }

        private static void CreateRequestHeaders(HttpRequestMessage request, IEnumerable<KeyValueModel> headers)
        {
            //System.Net.WebException: Could not create SSL/TLS secure channel
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#pragma warning disable SCS0004 // Certificate Validation has been disabled
            ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true;
#pragma warning restore SCS0004 // Certificate Validation has been disabled
            //set headers
            foreach (KeyValueModel item in headers)
            {
                request.Headers.TryAddWithoutValidation(item.Property, item.Value);
                Logger.Info($"HttpClientHelper Header added: {item.Property}: {item.Value}");
            }
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                return default;
            }

            using StreamReader sr = new StreamReader(stream);
            using JsonTextReader jtr = new JsonTextReader(sr);
            JsonSerializer js = new JsonSerializer();
            js.TypeNameHandling = TypeNameHandling.Objects;
            js.NullValueHandling = NullValueHandling.Ignore;
            T searchResult = js.Deserialize<T>(jtr);
            return searchResult;
        }

        public static HttpContent GetContent<T>(T model)
        {
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(model));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return content;
        }

        private static CancellationToken GetCancellationTokenSource()
        {
            CancellationTokenSource cts = new CancellationTokenSource(DefaultTimeout);
            cts.CancelAfter(DefaultTimeout);
            return cts.Token;
        }
    }

}
