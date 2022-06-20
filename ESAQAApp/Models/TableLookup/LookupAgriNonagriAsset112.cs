using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupAgriNonagriAsset112
    {
        [Key]
        public int AgriNonagriAsset112Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}