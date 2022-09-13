using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;
using Application.Services;
using Application.Builders;
using Application.Models;

namespace Application.Factories.Providers
{
    public class StroopwafelSupplierAProvider:  IStroopwafelSupplierProvider
    {
        private readonly IStroopwafelsApiService _stroopwafelsApiService;
        private static readonly string ProductsEndpoint = "Products";
        private static readonly string OrderEndpoint = "Order";
        public StroopwafelSupplierAProvider(IStroopwafelsApiService stroopwafelsApiService)
        {
            _stroopwafelsApiService = stroopwafelsApiService;
        }
        //public ISupplier Supplier => new SupplierA();

        public bool IsAvailable => true;

        public string Name => "Leverancier A";

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
            await _stroopwafelsApiService.PostOrder(OrderEndpoint,order);
        }

        public decimal GetShippingCost(Quote order)
        {
            return 5m;
        }
    }
}
