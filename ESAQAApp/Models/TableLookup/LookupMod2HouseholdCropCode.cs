using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2HouseholdCropCode
    {
        [Key]
        public int CropCodeId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}