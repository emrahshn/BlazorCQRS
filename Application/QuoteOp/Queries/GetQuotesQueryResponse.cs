using Application.Factories;
using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Application.QuoteOp.Queries
{
    public class GetQuotesQueryResponse
    {
        public GetQuotesQueryResponse()
        {
            OrderLines = new List<QuoteLine>();
        }
        public decimal TotalPrice { get; set; }

        public string TotalPricePresentation { get; set; }

        public decimal TotalWithoutShippingCost { get; set; }

        public IList<QuoteLine> OrderLines { get; set; }
        public bool SupplierIsAvailable { get; set; }

        public string SupplierName { get; set; }
    }
}
