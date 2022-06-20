using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod2Sec1Table21
    {
        [Key]
        public int Mod2Sec1Table21Id { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdAgriLand21Id { get; set; }

        public decimal? TotalLand { get; set; }
        public decimal? Crop_1 { get; set; }
        public decimal? Crop_2 { get; set; }
        public decimal? Crop_3 { get; set; }
    }
}