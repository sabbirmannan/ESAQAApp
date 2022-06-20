using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod3Table31
    {
        [Key]
        public int Mod3Table31Id { get; set; }
        public int MasterDataId { get; set; }        
        public int Mod3TypeOfChanges31Id { get; set; } //LookupMod3TypeOfChanges31
        public decimal? PresentCondition { get; set; }
        public int? Mod3ImpactOfSubProject31Id { get; set; } //LookupMod3ImpactOfSubProject31
    }
}