using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Stroopwafels.Shared.Enums
{
    public enum ServiceType
    {
        [Display(Name = "A")]
        A,
        [Display(Name = "B")]
        B,
        [Display(Name = "C")]
        C,
    }
}
