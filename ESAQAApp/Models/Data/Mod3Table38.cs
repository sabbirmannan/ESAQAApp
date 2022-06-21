using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models.Data
{
    public class Mod3Table38
    {
        [Key]
        public int Mod3Table38Id { get; set; }
        public int MasterDataId { get; set; }
        public int CurrentStatusSubProj38Id { get; set; } //LookupMod3CurrentStatusSubProj38
        public int? CurrentStatus38Id { get; set; } //LookupMod3CurrentStatus38
        public int? NeedRepairDigCanal { get; set; }  //1 = yes; 0 = no
    }
}