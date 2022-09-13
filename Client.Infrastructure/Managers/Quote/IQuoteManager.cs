using Application.QuoteOp.Queries;
using Stroopwafels.Shared.Models;

namespace Client.Infrastructure.Managers.Quote
{
    public interface IQuoteManager
    {
        Task<IResult<IList<GetQuotesQueryResponse>>> GetQuotesAsync(GetQuotesQueryRequest request);
    }
}