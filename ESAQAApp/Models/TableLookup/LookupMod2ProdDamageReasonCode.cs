using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2ProdDamageReasonCode
    {
        [Key]
        public int ProdDamageReasonCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}