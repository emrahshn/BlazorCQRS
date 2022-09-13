using Newtonsoft.Json;
using Stroopwafels.Shared.Enums;
using System.Text.Json.Serialization;

namespace Stroopwafels.Shared.Models.Stroopwafels
{
    public class Stroopwafel
    {
        [JsonProperty(PropertyName = "Type")]
        public StroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        public Brand Brand { get; set; }

        [JsonProperty(PropertyName = "Prijs")]
        public decimal Price { get; set; }
    }
}
