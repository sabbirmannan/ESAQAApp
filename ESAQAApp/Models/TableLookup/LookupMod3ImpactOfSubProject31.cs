﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod3ImpactOfSubProject31
    {
        [Key]
        public int Mod3ImpactOfSubProject31Id { get; set; }

        [StringLength(150)]
        public string OptionName { get; set; }
    }
}