using Stroopwafels.Shared.Enums;

namespace Data.Entities
{
    public class Orders : BaseEntity
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public StroopwafelType Type { get; set; }
        public Brand Brand { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
        public DateTime WishDate { get; set; }
    }
}
