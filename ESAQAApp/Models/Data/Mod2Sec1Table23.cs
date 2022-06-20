using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod2Sec1Table23
    {
        [Key]
        public int Mod2Sec1Table23Id { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdLandTypeId { get; set; }
        public int CropCodeId { get; set; }

        public string OtherCropCode { get; set; }

        public decimal? LandAmntIrrigatedLand { get; set; }
        public decimal? LandAmntWithoutIrrigation { get; set; }

        public decimal? CropProdAmntIrrigatedLand { get; set; }
        public decimal? CropProdAmntWithoutIrrigation { get; set; }

        public decimal? TotalValueCropReceived { get; set; }
        public decimal? TotalValueByproducts { get; set; }
        public decimal? TotalCropYield { get; set; }
        public decimal? ShareholderShare { get; set; }
    }
}