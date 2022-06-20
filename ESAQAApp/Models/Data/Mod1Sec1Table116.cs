using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod1Sec1Table116
    {
        [Key]
        public int Mod1Sec1Table116Id { get; set; }
        public int MasterDataId { get; set; }
        public int FoodConsumpExps116Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public decimal? AmountOfConsumption { get; set; }
        public decimal? ConsumptionValue { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}