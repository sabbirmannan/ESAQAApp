using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2HouseholdLandType
    {
        [Key]
        public int HouseholdLandTypeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}