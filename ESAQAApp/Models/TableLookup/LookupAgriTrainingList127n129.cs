﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupAgriTrainingList127n129
    {
        [Key]
        public int LookupAgriTrainingListId { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}