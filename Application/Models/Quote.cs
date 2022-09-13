using Application.Factories;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.Models
{
    public class Quote
    {
        public Quote(IList<QuoteLine> orderLines, IStroopwafelSupplierProvider provider)
        {
            OrderLines = orderLines;
            Provider = provider;

        }
        public decimal TotalPrice => TotalWithoutShippingCost + Provider.GetShippingCost(this);

        public string TotalPricePresentation => TotalPrice.ToString("C");

        public decimal TotalWithoutShippingCost
        {
            get { return OrderLines.Sum(o => o.Price); }
        }

        public IList<QuoteLine> OrderLines { get; }
        public IStroopwafelSupplierProvider Provider { get; }
    }
}
