using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2HouseholdAgriLandType22
    {
        [Key]
        public int HouseholdAgriLandType22Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}