using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3FuelCode35
    {
        [Key]
        public int FuelCode35Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}