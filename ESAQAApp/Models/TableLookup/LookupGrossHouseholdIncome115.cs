using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupGrossHouseholdIncome115
    {
        [Key]
        public int GrossHouseholdIncome115Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}