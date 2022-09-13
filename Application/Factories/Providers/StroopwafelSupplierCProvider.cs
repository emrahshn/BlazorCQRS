using Stroopwafels.Shared.Enums;
using Application.Builders;
using Application.Models;
using Application.Services;

namespace Application.Factories.Providers
{
    public class StroopwafelSupplierCProvider: IStroopwafelSupplierProvider
    {
        private readonly IStroopwafelsApiService _stroopwafelsApiService;
        private static readonly string ProductsEndpoint = "Products";
        private static readonly string OrderEndpoint = "Order";
        private const int ShippingCostPercentage = 5;
        public StroopwafelSupplierCProvider(IStroopwafelsApiService stroopwafelsApiService) 
        {
            _stroopwafelsApiService = stroopwafelsApiService;
        }
        public string Name => "Leverancier C";

        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost / 100 * ShippingCostPercentage;
        }

        public bool IsAvailable => true;

        public async Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var stroopwafels = await _stroopwafelsApiService.GetProducts(ProductsEndpoint);
            var builder = new QuoteBuilder();
            return builder.CreateOrder(orderDetails, stroopwafels!, this);
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            await _stroopwafelsApiService.PostOrder(OrderEndpoint, order);
        }
    }
}
