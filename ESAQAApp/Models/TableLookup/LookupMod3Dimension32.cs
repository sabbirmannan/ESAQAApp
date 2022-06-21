using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3Dimension32
    {
        [Key]
        public int Mod3Dimension32Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}