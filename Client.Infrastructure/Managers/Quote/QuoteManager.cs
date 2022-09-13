using Application.QuoteOp.Queries;
using Client.Infrastructure.Routes;
using Stroopwafels.Shared.Extensions;
using Stroopwafels.Shared.Models;
using System.Net.Http.Json;

namespace Client.Infrastructure.Managers.Quote
{
    public class QuoteManager : IQuoteManager
    {
        private readonly HttpClient _httpClient;
        public QuoteManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<IList<GetQuotesQueryResponse>>> GetQuotesAsync(GetQuotesQueryRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(QuoteEndpoints.Get, request);
            return await response.ToResult<IList<GetQuotesQueryResponse>>();
        }
    }
}
