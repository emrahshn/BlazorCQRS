using Stroopwafels.Shared.Models;
using MediatR;
using Data.Context;
using System.Linq.Expressions;
using Data.Entities;
using System.Xml.Linq;
using Stroopwafels.Shared.Enums;
using System;

namespace Application.OrderOp.Queries
{
    public class GetOrdersQuery : IRequest<Result<IList<GetOrdersQueryResponse>>>
    {
    }

    internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<IList<GetOrdersQueryResponse>>>
    {
        private readonly WafelsDbContext _db;
        public GetOrdersQueryHandler(WafelsDbContext db)
        {
            _db = db;
        }

        public async Task<Result<IList<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Orders, GetOrdersQueryResponse>> expression = e => new GetOrdersQueryResponse
            {
                Id = e.Id,
                Name = e.Name,
                OrderId = e.OrderId,
                WishDate = e.WishDate,
                Price = e.Price,
                Amount = e.Amount,
                Supplier = e.Supplier,
                CreatedOn = e.CreatedOn,
                Type = e.Type,
                Brand = e.Brand,
                Active = e.Active,
                isDeleted = e.isDeleted
            };
            var result = _db.Orders.Select(expression).ToList();
            return await Result<IList<GetOrdersQueryResponse>>.SuccessAsync(result);
        }
    }
}
