using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupSanitaryType135
    {
        [Key]
        public int SanitaryType135Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}