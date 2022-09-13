using Stroopwafels.Shared.Enums;
using Application.Services;
using Application.Builders;
using Application.Models;
using Stroopwafels.Shared.Models;
using Microsoft.Extensions.Options;
using Stroopwafels.Shared.Helpers;

namespace Application.Factories.Providers
{
    public class StroopwafelSupplierBProvider : IStroopwafelSupplierProvider
    {
        private readonly IStroopwafelsApiService _stroopwafelsApiService;
        private readonly ServiceInformation _serviceInfo;
        private const decimal ShippingCostLimit = 50m;
        private const decimal ShippingCostAboveLimit = 0m;
        private const decimal ShippingCostUnderLimit = 5m;
        public StroopwafelSupplierBProvider(IStroopwafelsApiService stroopwafelsApiService,
            IOptions<ServiceInformation> serviceInfo)
        {
            _stroopwafelsApiService = stroopwafelsApiService;
            _serviceInfo = serviceInfo.Value;
        }
        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        };
        public string Name => "Leverancier B";

        public bool IsAvailable => GetAvailability();

        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost > ShippingCostLimit ? ShippingCostAboveLimit : ShippingCostUnderLimit;
        }

        private bool GetAvailability()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var isHoliday = PublicHolidays.Any(h => (h.Month == DateTime.Now.Date.Month && h.Day == DateTime.Now.Date.Day));
            return !isHoliday;
        }
        public async Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var stroopwafels = await _stroopwafelsApiService.GetProducts(_serviceInfo.ProductsEndpoint);
            var builder = new QuoteBuilder();
            return builder.CreateOrder(orderDetails, stroopwafels!, this);
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            await _stroopwafelsApiService.PostOrder(_serviceInfo.OrderEndpoint, order);
        }
    }
}