using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec5Table137
    {
        [Key]
        public int Mod1Sec5Table137Id { get; set; }
        public int MasterDataId { get; set; }
        public int WorkForLivingJobType137Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public int? TotalDays { get; set; }
        public int? AvgHour { get; set; }
        public decimal? ApproxEarnLastYear { get; set; }
    }
}