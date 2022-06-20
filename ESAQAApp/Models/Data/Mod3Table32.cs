using System.ComponentModel.DataAnnotations;

namespace BAC007.Models.Data
{
    public class Mod3Table32
    {
        [Key]
        public int Mod3Table31Id { get; set; }
        public int MasterDataId { get; set; }        
        public int Mod3NaturalDisaster32Id { get; set; } //LookupMod3NaturalDisaster32
        public int? Mod3Recurrence32Id { get; set; } //LookupMod3Recurrence32
        public int? Mod3Extension32Id { get; set; } //LookupMod3Extension32
        public int? Mod3Dimension32Id { get; set; } //LookupMod3Dimension32
    }
}