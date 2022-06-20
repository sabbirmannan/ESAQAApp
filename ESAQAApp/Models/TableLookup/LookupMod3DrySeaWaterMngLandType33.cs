using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3DrySeaWaterMngLandType33
    {
        [Key]
        public int Mod3DrySeaWaterMngLandType33Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}