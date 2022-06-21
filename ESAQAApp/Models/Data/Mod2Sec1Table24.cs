using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod2Sec1Table24
    {
        [Key]
        public int Mod2Sec1Table24Id { get; set; }
        public int MasterDataId { get; set; }
        public int T24HouseholdLandTypeId { get; set; }
        public int T24CropCodeId { get; set; }

        public string OtherT24CropCode { get; set; }

        public decimal? UreaAmount { get; set; }
        public decimal? UreaValue { get; set; }
        public decimal? PotashTspAmount { get; set; }
        public decimal? PotashTspValue { get; set; }
        public decimal? PesticidesAmount { get; set; }
        
        public decimal? TotalCostIrrigation { get; set; }
        public decimal? SeedsAmount { get; set; }
        public decimal? SeedsValue { get; set; }

        public decimal? PowerTillerTaka { get; set; }

        public int? SelfEmployedLaborDays { get; set; }
        public int? RentLaborDays { get; set; }
        public decimal? DailyLaborCost { get; set; }
        public decimal? TotalCost { get; set; }
    }
}