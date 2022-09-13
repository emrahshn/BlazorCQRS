using Newtonsoft.Json;

namespace Stroopwafels.Shared.Models.Stroopwafels
{
    public class QuoteLine
    {
        public QuoteLine(int amount, Stroopwafel stroopwafel)
        {
            Amount = amount;
            Stroopwafel = stroopwafel;
        }
        public int Amount { get; }

        [JsonProperty(PropertyName = "Product")]
        public Stroopwafel Stroopwafel { get; }

        public decimal Price => Amount * Stroopwafel.Price;
    }
}
