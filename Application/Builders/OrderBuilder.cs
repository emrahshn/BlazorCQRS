using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Builders
{
    public class OrderBuilder
    {
        public Order CreateOrder(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var orderLines = quoteLines.Select(MapToOrderLine).ToList();
            return new Order(orderLines);
        }

        private static OrderLine MapToOrderLine(KeyValuePair<StroopwafelType, int> line)
        {
            return new OrderLine(line.Value, new OrderProduct(line.Key));
        }
    }
}
