using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod2Sec2Table27
    {
        [Key]
        public int Mod2Sec2Table27Id { get; set; }
        public int MasterDataId { get; set; }

        public int CropCodeId { get; set; }
        public int? CropProcessingId { get; set; }
        public int? CropDryProcessingId { get; set; }
        public int? CropStoreProcessingId { get; set; }
        public int? CropMarketingId { get; set; }
    }
}