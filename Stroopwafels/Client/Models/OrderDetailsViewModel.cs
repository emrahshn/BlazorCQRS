using Stroopwafels.Shared.Enums;

namespace Stroopwafels.Client.Models
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel()
        {
            OrderRows = new List<OrderRow>
            {
                new OrderRow(0, StroopwafelType.Gewoon),
                new OrderRow(0, StroopwafelType.Suikervrij),
                new OrderRow(0, StroopwafelType.Super)
            };
        }
        public IList<OrderRow> OrderRows { get; set; }
    }
}
