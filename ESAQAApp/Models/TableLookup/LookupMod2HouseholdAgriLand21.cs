using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2HouseholdAgriLand21
    {
        [Key]
        public int HouseholdAgriLand21Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}