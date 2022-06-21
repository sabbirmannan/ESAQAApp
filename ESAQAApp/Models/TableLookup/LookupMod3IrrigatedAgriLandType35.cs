using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod3IrrigatedAgriLandType35
    {
        [Key]
        public int Mod3IrrigatedAgriLandType35Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}