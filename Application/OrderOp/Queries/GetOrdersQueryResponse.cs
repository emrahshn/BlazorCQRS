using Stroopwafels.Shared.Enums;

namespace Application.OrderOp.Queries
{
    public class GetOrdersQueryResponse
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public StroopwafelType Type { get; set; }
        public Brand Brand { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
        public DateTime WishDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public bool isDeleted { get; set; }
    }
}
