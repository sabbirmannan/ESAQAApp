﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupMod2CropMarketing
    {
        [Key]
        public int CropMarketingId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}