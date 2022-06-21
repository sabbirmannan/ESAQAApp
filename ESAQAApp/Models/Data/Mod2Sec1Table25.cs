using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod2Sec1Table25
    {
        [Key]
        public int Mod2Sec1Table25Id { get; set; }
        public int MasterDataId { get; set; }

        public int T25CropCodeId { get; set; }
        public int? WaterSourceCodeId { get; set; }
        public int? IrrigationSysCodeId { get; set; }
        public int? AvailabilityCodeId { get; set; }
    }
}