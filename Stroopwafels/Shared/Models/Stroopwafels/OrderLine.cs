namespace Stroopwafels.Shared.Models.Stroopwafels
{
    public class OrderLine
    {
        public OrderLine(int amount, OrderProduct product)
        {
            Amount = amount;
            Product = product;
        }
        public int Amount { get; }

        public OrderProduct Product { get; }
    }
}
