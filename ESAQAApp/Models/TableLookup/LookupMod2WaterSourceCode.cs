using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2WaterSourceCode
    {
        [Key]
        public int WaterSourceCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}