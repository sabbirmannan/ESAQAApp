using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMod2CropProcessing
    {
        [Key]
        public int CropProcessingId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}