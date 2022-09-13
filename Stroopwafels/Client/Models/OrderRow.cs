using Stroopwafels.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Client.Models
{
    public class OrderRow
    {
        public OrderRow(int amount, StroopwafelType type)
        {
            Amount = amount;
            Type = type;
        }
        public OrderRow()
        {
        }

        [Required]
        public int Amount { get; set; }

        public StroopwafelType Type { get; set; }

        public string DisplayName => Type.ToString();
    }
}