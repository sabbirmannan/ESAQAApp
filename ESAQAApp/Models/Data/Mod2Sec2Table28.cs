using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod2Sec2Table28
    {
        [Key]
        public int Mod2Sec2Table28Id { get; set; }
        public int MasterDataId { get; set; }        
        public int CropCodeId { get; set; }

        public decimal? TotalCropProduction { get; set; }
        public decimal? TotalCropProdSelfUse { get; set; }
        public decimal? RestMarketCropProd { get; set; }
        public decimal? FieldSaleCropProdAmount { get; set; }
        public decimal? FieldSaleCropProdValue { get; set; }
        public string MarketName { get; set; }
        
        public decimal? DistanceOfLandToMarket { get; set; }
        public decimal? TravelCostPerMon { get; set; }
        public decimal? TotalCost { get; set; }       
    }
}