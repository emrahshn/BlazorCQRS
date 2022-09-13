using Stroopwafels.Shared.Enums;

namespace Application.QuoteOp.Queries
{
    public class GetQuotesQueryRequest
    {
        public GetQuotesQueryRequest(IList<KeyValuePair<StroopwafelType, int>> orderLines)
        {
            OrderLines = orderLines;
        }
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }
    }
}
