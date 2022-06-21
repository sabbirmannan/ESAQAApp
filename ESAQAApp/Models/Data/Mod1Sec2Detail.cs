using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec2Detail
    {
        [Key]
        public int Mod1Sec2DetailId { get; set; }

        public int MasterDataId { get; set; }

        public int? MainOccupation { get; set; } //1.13

        public int? TotalMonthEngage { get; set; } //1.14
        public int? HasLastYearLoan { get; set; } //1.18
    }
}