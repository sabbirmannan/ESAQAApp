using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec1Table115
    {
        [Key]
        public int Mod1Sec1Table115Id { get; set; }
        public int MasterDataId { get; set; }
        public int GrossHouseholdIncome115Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public decimal? MonthlyIncome { get; set; }
        public decimal? YearlyIncome { get; set; }
    }
}