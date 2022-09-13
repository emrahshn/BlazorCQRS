namespace Stroopwafels.Shared.Models.Stroopwafels
{
    public class Order
    {
        public Order(IList<OrderLine> productsAndAmounts)
        {
            ProductsAndAmounts = productsAndAmounts;
        }
        public IList<OrderLine> ProductsAndAmounts { get; }
    }
}
