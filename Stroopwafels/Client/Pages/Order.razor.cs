using Application.OrderOp.Queries;
using Client.Infrastructure.Managers.Orders;
using Microsoft.AspNetCore.Components;

namespace Stroopwafels.Client.Pages
{
    public partial class Order : ComponentBase
    {
        public List<GetOrdersQueryResponse> Orders { get; set; } = new();
        [Inject] IOrdersManager _orderManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var data = await _orderManager.GetOrdersAsync();
            if (data.Succeeded)
            {
                Orders = data.Data.Where(x=>(!x.isDeleted&&x.Active)).OrderByDescending(x=>x.CreatedOn).ToList();
            }
        }
    }
}
