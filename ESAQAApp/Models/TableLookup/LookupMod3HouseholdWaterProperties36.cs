using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3HouseholdWaterProperties36
    {
        [Key]
        public int HouseholdWaterProperties36Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}