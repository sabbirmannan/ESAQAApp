using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3HouseholdOwnershipCode36
    {
        [Key]
        public int HouseholdOwnershipCode36Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}