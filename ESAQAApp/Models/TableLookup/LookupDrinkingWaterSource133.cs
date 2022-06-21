using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupDrinkingWaterSource133
    {
        [Key]
        public int DrinkingWaterSource133Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}