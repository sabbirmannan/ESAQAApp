using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod2Sec3Table29
    {
        [Key]
        public int Mod2Sec3Table29Id { get; set; }
        public int MasterDataId { get; set; }        
        public int CropCodeId { get; set; }
        public int? ProdDamageCodeId { get; set; }
        public int? ProdDamageReasonCodeId { get; set; }
    }
}