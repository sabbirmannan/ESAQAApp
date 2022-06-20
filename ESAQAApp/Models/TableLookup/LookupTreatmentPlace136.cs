using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupTreatmentPlace136
    {
        [Key]
        public int TreatmentPlace136Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}