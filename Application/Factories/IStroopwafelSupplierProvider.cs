using Application.Factories.Providers;
using Application.Models;
using Newtonsoft.Json;
using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Factories
{
    public interface IStroopwafelSupplierProvider
    {
        //ISupplier Supplier { get; }
        string Name { get; }
        bool IsAvailable { get; }
        decimal GetShippingCost(Quote order);

        Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails);

        Task Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
