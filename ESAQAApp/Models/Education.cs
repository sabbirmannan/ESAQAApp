using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]       
        [StringLength(50)]
        [Display(Name = "Education")]
        public string EducationName { get; set; }
    }
}