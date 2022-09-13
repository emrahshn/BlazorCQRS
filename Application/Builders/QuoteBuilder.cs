using Application.Factories;
using Application.Models;
using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Builders
{
    public class QuoteBuilder
    {
        public Quote CreateOrder(IList<KeyValuePair<StroopwafelType, int>> orderDetails, IList<Stroopwafel> stroopwafels, IStroopwafelSupplierProvider provider)
        {
            var orderLines = new List<QuoteLine>();

            foreach (var orderLine in orderDetails)
            {
                var stroopwafel = stroopwafels.First(s => s.Type == orderLine.Key);
                orderLines.Add(new QuoteLine(orderLine.Value, stroopwafel));
            }
            return new Quote(orderLines, provider);
        }
    }
}
