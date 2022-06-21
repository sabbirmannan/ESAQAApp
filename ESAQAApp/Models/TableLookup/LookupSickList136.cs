using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupSickList136
    {
        [Key]
        public int SickList136Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}