using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec4Detail
    {
        [Key]
        public int Mod1Sec4DetailId { get; set; }

        public int MasterDataId { get; set; }

        public int? RoofHouseStructureTypeId { get; set; } //1.31 1
        public int? WallHouseStructureTypeId { get; set; } //1.31 2
        public int? FloorHouseStructureTypeId { get; set; } //1.31 3
       
        public int? HasElectricity { get; set; } //1.32

        public string DrinkingWaterSource { get; set; } //1.33
        public string OtherDrinkingWaterSource { get; set; } //1.33 other

        public decimal? PercentOfHomeCookingFuel { get; set; } //1.34

        public string UsingSanitary { get; set; } //1.35
                                                 
        public int? AnyoneSickLastYear { get; set; } //1.36

        public string SickList { get; set; } //1.36 1
        public string OtherSickList { get; set; } //1.36 1 other

        public int? HasTakenTreatmentForIllness { get; set; } //1.36 2

        public string TreatmentList { get; set; } //1.36 2
        public string OtherTreatmentList { get; set; } //1.36 2 other      
    }
}