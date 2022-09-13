using Application.OrderOp.Queries;
using Client.Infrastructure.Routes;
using Stroopwafels.Shared.Extensions;
using Stroopwafels.Shared.Models;
using System.Net.Http.Json;

namespace Client.Infrastructure.Managers.Orders
{
    public class OrdersManager : IOrdersManager
    {
        private readonly HttpClient _httpClient;
        public OrdersManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<IList<GetOrdersQueryResponse>>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync(OrdersEndpoints.Get);
            return await response.ToResult<IList<GetOrdersQueryResponse>>();
        }
        public async Task<IResult<Guid>> SaveAsync(AddEditOrdersCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(OrdersEndpoints.Save, request);
            return await response.ToResult<Guid>();
        }
    }
}
