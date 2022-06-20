using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2CropProcessing
    {
        [Key]
        public int CropProcessingId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}