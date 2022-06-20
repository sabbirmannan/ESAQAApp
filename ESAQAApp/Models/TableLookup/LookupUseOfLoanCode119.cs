using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupUseOfLoanCode119
    {
        [Key]
        public int UseOfLoanCode119Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}