using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Mod1Sec1Table117
    {
        [Key]
        public int Mod1Sec1Table116Id { get; set; }
        public int MasterDataId { get; set; }
        public int StatementOfExpenditure117Id { get; set; }

        [StringLength(250)]
        public string Others { get; set; }

        public decimal? TotalAmount { get; set; }        
    }
}