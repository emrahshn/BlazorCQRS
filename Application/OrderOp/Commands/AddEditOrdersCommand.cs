using Stroopwafels.Shared.Models;
using MediatR;
using Stroopwafels.Shared.Models.Stroopwafels;
using Data.Context;
using Data.Entities;
using Application.Services;
using Microsoft.Extensions.Options;
using Stroopwafels.Shared.Enums;
using Application.Factories;
using Application.Models;

namespace Application.OrderOp.Queries
{
    public partial class AddEditOrdersCommand : IRequest<Result<Guid>>
    {
        public AddEditOrdersCommand(List<OrderLine> productsAndAmounts)
        {
            ProductsAndAmounts = productsAndAmounts;
        }
        public int Id { get; set; }
        public List<OrderLine> ProductsAndAmounts { get; set; }
        public Guid OrderId { get; set; }
        public DateTime WishDate { get; set; }
        public string Supplier { get; set; }
        public string Name { get; set; }
    }

    internal class AddEditOrdersCommandHandler : IRequestHandler<AddEditOrdersCommand, Result<Guid>>
    {
        private readonly WafelsDbContext _db;
        private readonly IStroopwafelSupplierProviderFactory _stroopwafelSupplierProviderFactory;
        private readonly IMailService _mailService;
        public AddEditOrdersCommandHandler(WafelsDbContext db,
            IStroopwafelSupplierProviderFactory stroopwafelSupplierProviderFactory,
            IMailService mailService)
        {
            _db = db;
            _stroopwafelSupplierProviderFactory = stroopwafelSupplierProviderFactory;
            _mailService = mailService;
        }
        public async Task<Result<Guid>> Handle(AddEditOrdersCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                //db operations
                foreach(var item in command.ProductsAndAmounts)
                {
                    Orders order = new Orders()
                    {
                        OrderId = command.OrderId,
                        Name = command.Name,
                        Supplier = command.Supplier,
                        WishDate = command.WishDate,
                        Type = item.Product.Type,
                        Brand = item.Product.Brand,
                        Price = item.Product.Price,
                        Amount = item.Amount,
                        Active = true,
                        CreatedOn = DateTime.Now,
                        isDeleted = false,
                    };
                    await _db.Orders.AddAsync(order);
                }
                _db.SaveChanges();

                //Api calls
                var provider = _stroopwafelSupplierProviderFactory.Create(ServiceType.A);
                var pairs = command.ProductsAndAmounts.Select(x => new KeyValuePair<StroopwafelType, int>(x.Product.Type, x.Amount)).ToList();
                await provider.Order(pairs);

                //Mali sender
                var mailRequest = new MailRequest
                {
                    From = "shn.emrah27@gmail.com",
                    To = "stroopwafel@t-mobile.nl",
                    Body = String.Format("you have a new order. Order Id:{0}", command.OrderId),
                    Subject = "New Order"
                };
                await _mailService.SendAsync(mailRequest);
                return await Result<Guid>.SuccessAsync(command.OrderId, "Order Saved");
            }
            else
            {
                return await Result<Guid>.FailAsync();
            }
        }
    }
}
