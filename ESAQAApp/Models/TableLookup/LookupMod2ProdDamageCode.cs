using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2ProdDamageCode
    {
        [Key]
        public int ProdDamageCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}