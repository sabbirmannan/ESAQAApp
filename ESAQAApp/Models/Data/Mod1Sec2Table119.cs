using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod1Sec2Table119
    {
        [Key]
        public int Mod1Sec2Table119Id { get; set; }
        public int MasterDataId { get; set; }
        public int LoanSource119Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public decimal? LoanAmount { get; set; }

        public int? UseOfLoanCode119Id { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public decimal? AvgInterest { get; set; }
    }
}