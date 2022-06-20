using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupTrainingOrganization122
    {
        [Key]
        public int TrainingOrganization122Id { get; set; }

        [StringLength(250)]
        public string OptionName { get; set; }
    }
}