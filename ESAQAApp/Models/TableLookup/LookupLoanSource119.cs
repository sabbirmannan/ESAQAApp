using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupLoanSource119
    {
        [Key]
        public int LoanSource119Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}