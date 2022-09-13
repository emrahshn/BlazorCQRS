using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stroopwafels.Shared.Enums;

namespace Stroopwafels.Shared.Models.Stroopwafels
{
    public class OrderProduct
    {
        public OrderProduct(StroopwafelType type)
        {
            Type = type;
            Brand = Brand.Stroopie;
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public StroopwafelType Type { get; }

        [JsonProperty(PropertyName = "Merk")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Brand Brand { get; set; }
        public decimal Price { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
