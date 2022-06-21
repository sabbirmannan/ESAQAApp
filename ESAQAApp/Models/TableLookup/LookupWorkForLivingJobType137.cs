using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupWorkForLivingJobType137
    {
        [Key]
        public int WorkForLivingJobType137Id { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}