using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod3Table36
    {
        [Key]
        public int Mod3Table36Id { get; set; }
        public int MasterDataId { get; set; }
        public int HouseholdWaterUse36Id { get; set; } //LookupMod3HouseholdWaterUse36
        public int? HouseholdWaterSource36Id { get; set; } //LookupMod3HouseholdWaterSource36
        public int? HouseholdOwnershipCode36Id { get; set; } //LookupMod3HouseholdOwnershipCode36
        public int? HouseholdWaterProperties36Id { get; set; } //LookupMod3HouseholdWaterProperties36
        public int? HouseholdWaterArsenic36Id { get; set; } //LookupMod3HouseholdWaterArsenic36
    }
}