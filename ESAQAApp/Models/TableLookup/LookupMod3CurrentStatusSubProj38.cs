using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3CurrentStatusSubProj38
    {
        [Key]
        public int CurrentStatusSubProj38Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}