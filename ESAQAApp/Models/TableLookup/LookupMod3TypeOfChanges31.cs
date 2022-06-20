using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3TypeOfChanges31
    {
        [Key]
        public int Mod3TypeOfChanges31Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}