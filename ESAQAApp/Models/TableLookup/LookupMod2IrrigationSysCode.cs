using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2IrrigationSysCode
    {
        [Key]
        public int IrrigationSysCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}