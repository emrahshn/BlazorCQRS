using Stroopwafels.Shared.Models;
using MediatR;
using Stroopwafels.Shared.Enums;
using Application.Factories;
using Application.Models;

namespace Application.QuoteOp.Queries
{
    public class GetQuotesQuery : IRequest<Result<List<GetQuotesQueryResponse>>>
    {
        public GetQuotesQueryRequest QuotesQueryRequest { get; set; }
        public GetQuotesQuery(GetQuotesQueryRequest quotesQueryRequest)
        {
            QuotesQueryRequest = quotesQueryRequest;
        }
    }

    internal class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, Result<List<GetQuotesQueryResponse>>>
    {
        private readonly IStroopwafelSupplierProviderFactory _stroopwafelSupplierProviderFactory;
        public GetQuotesQueryHandler(IStroopwafelSupplierProviderFactory stroopwafelSupplierProviderFactory)
        {
            _stroopwafelSupplierProviderFactory = stroopwafelSupplierProviderFactory;
        }

        public async Task<Result<List<GetQuotesQueryResponse>>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
        {
            List<Quote> QuoteList = new List<Quote>();
            foreach(var type in Enum.GetValues(typeof(ServiceType)))
            {
                var provider = _stroopwafelSupplierProviderFactory.Create((ServiceType)type);
                var data = await provider.GetQuote(request.QuotesQueryRequest.OrderLines);
                QuoteList.Add(data);
            }
            List<GetQuotesQueryResponse> result = QuoteList.Select(x => new GetQuotesQueryResponse
            {
                TotalPrice =x.TotalPrice,
                TotalPricePresentation = x.TotalPricePresentation,
                TotalWithoutShippingCost = x.TotalWithoutShippingCost,
                OrderLines = x.OrderLines,
                SupplierIsAvailable = x.Provider.IsAvailable,
                SupplierName = x.Provider.Name
            }).ToList();
            return await Result<List<GetQuotesQueryResponse>>.SuccessAsync(result);
        }
    }
}
