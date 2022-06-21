using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class LookupTrainingOrganization122
    {
        [Key]
        public int TrainingOrganization122Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}