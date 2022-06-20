using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupDrinkingWaterSource133
    {
        [Key]
        public int DrinkingWaterSource133Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}