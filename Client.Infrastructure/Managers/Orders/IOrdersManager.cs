using Application.OrderOp.Queries;
using Stroopwafels.Shared.Models;

namespace Client.Infrastructure.Managers.Orders
{
    public interface IOrdersManager
    {
        Task<IResult<IList<GetOrdersQueryResponse>>> GetOrdersAsync();
        Task<IResult<Guid>> SaveAsync(AddEditOrdersCommand request);
    }
}