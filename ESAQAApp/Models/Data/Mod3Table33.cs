using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod3Table33
    {
        [Key]
        public int Mod3Table33Id { get; set; }
        public int MasterDataId { get; set; }        
        public int Mod3DrySeaWaterMngLandType33Id { get; set; } //LookupMod3DrySeaWaterMngLandType33
        public decimal? AmountCultivatedAgriLand { get; set; }
    }
}