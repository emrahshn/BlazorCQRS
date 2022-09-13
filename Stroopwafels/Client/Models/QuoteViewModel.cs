using Application.QuoteOp.Queries;

namespace Stroopwafels.Client.Models
{
    public class QuoteViewModel
    {
        public IList<Quote> Quotes { get; set; }
        public GetQuotesQueryResponse MatchedSupplier { get; set; }

        public IList<OrderRow> OrderRows { get; set; }

        public string SelectedSupplier { get; set; }
        public int PetersEarnings { get; set; }

        public QuoteViewModel()
        {
            Quotes = new List<Quote>();
            MatchedSupplier = new GetQuotesQueryResponse();
            OrderRows = new List<OrderRow>();
            SelectedSupplier = null!;
        }
    }
}
