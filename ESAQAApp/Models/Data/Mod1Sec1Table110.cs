using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod1Sec1Table110
    {
        [Key]
        public int Mod1Sec1Table110Id { get; set; }
        public int MasterDataId { get; set; }
        public int MovablePropertyOptionId { get; set; }

        [StringLength(250)]
        public string Others { get; set; }
        public decimal? LandDecimal { get; set; }
        public decimal? LandPresentValue { get; set; }
    }
}