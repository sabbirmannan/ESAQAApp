using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3NaturalDisaster32
    {
        [Key]
        public int Mod3NaturalDisaster32Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}