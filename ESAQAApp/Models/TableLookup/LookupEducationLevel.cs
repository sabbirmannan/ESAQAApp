using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class LookupEducationLevel
    {
        [Key]
        public int EducationLevelId { get; set; }

        [StringLength(150)]        
        public string EducationLevelName { get; set; }

        [StringLength(50)]
        public string EducationLevelShortName { get; set; }
    }
}