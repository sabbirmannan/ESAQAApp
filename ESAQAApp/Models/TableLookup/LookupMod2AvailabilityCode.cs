using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2AvailabilityCode
    {
        [Key]
        public int AvailabilityCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}