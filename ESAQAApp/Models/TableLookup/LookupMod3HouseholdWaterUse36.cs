using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3HouseholdWaterUse36
    {
        [Key]
        public int HouseholdWaterUse36Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}