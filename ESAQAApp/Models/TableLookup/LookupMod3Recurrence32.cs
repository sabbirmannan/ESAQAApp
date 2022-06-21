using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3Recurrence32
    {
        [Key]
        public int Mod3Recurrence32Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}