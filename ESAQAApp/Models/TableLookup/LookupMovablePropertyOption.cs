using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupMovablePropertyOption
    {
        [Key]
        public int OptionID { get; set; }

        [StringLength(50)]
        public string OptionName { get; set; }
    }
}