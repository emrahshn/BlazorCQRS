using Application.OrderOp.Queries;
using Application.QuoteOp.Queries;
using BlazorDateRangePicker;
using Client.Infrastructure.Managers.Orders;
using Client.Infrastructure.Managers.Quote;
using Microsoft.AspNetCore.Components;
using Stroopwafels.Client.Models;
using Stroopwafels.Shared.Enums;
using Stroopwafels.Shared.Models.Stroopwafels;

namespace Stroopwafels.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] IQuoteManager _quoteManager { get; set; }
        [Inject] IOrdersManager _orderManager { get; set; }
        public OrderDetailsViewModel Model { get; set; } = new();
        public QuoteViewModel ViewModel { get; set; } = new();
        public IList<Stroopwafel> Stroopwafels { get; set; }
        public DateTimeOffset? WishDate { get; set; } = null;
        public string Name { get; set; }
        public DateRangePicker Picker { get; set; } = new();
        public bool Busy { get; set; } = false;
        private static readonly List<DateTimeOffset> PublicHolidays = new List<DateTimeOffset>
        {
            new DateTimeOffset(new DateTime(2016, 1, 1), TimeSpan.Zero),
            new DateTimeOffset(new DateTime(2016, 12, 25), TimeSpan.Zero),
            new DateTimeOffset(new DateTime(2016, 12, 26), TimeSpan.Zero)
        };
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
        }
        async void FormSubmitted()
        {
            Busy = true;
            var orderDetails = GetOrderDetails(Model.OrderRows);
            var quotes = await GetQuotesFor(orderDetails);

            foreach (var quote in quotes)
            {
                ViewModel.Quotes.Add(new Models.Quote
                {
                    SupplierName = quote.SupplierName,
                    TotalAmount = quote.TotalPricePresentation
                });
            }
            ViewModel.MatchedSupplier = await GetMatchedQuote(quotes);
            ViewModel.PetersEarnings = Model.OrderRows.Sum(x => x.Amount);
            ViewModel.OrderRows = Model.OrderRows;
            ViewModel.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().SupplierName;
            Busy = false;
            StateHasChanged();
        }
        private async Task<IList<GetQuotesQueryResponse>> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new GetQuotesQueryRequest(orderDetails);
            var orders = await _quoteManager.GetQuotesAsync(query);
            if (orders.Succeeded)
            {
                return orders.Data;
            }
            return null;
        }

        private static IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IEnumerable<OrderRow> orderRows)
        {
            return orderRows
                .Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount))
                .ToList();
        }
        public void DepartureDateSelected(DateRange range)
        {
            WishDate = range.Start;
            StateHasChanged();
        }
        private bool DaysEnabledFunction(DateTimeOffset day)
        {
            return !PublicHolidays.Any(x => (x.Day == day.Day && x.Month == day.Month));
        }
        private async Task<GetQuotesQueryResponse> GetMatchedQuote(IList<GetQuotesQueryResponse> quotes)
        {
            quotes = quotes.OrderBy(x => x.TotalPrice).ToList();
            return quotes.First(x => x.SupplierIsAvailable);
        }
        private async Task SubmitOrder()
        {
            AddEditOrdersCommand command = new AddEditOrdersCommand(ViewModel.MatchedSupplier.OrderLines.Select(x=>
            new OrderLine(x.Amount,new OrderProduct(x.Stroopwafel.Type) {Brand= x.Stroopwafel.Brand,Price=x.Price})).ToList())
            {
                Id = 0,
                WishDate = WishDate.Value.Date,
                OrderId = Guid.NewGuid(),
                Supplier = ViewModel.MatchedSupplier.SupplierName,
                Name = Name,
            };
            await _orderManager.SaveAsync(command);
        }
    }
}
