using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAC007.Models
{
    public class District
    {
        [Key]
        [StringLength(4, ErrorMessage = "District code cannot be longer than 4 characters.")]
        public string DistrictCode { get; set; }

        [Required(ErrorMessage = "District name is required!")]
        [Display(Name = "District")]
        [StringLength(100, ErrorMessage = "District name cannot be longer than 100 characters.")]
        public string DistrictName { get; set; }

        [Required(ErrorMessage = "Division is required!")]
        [StringLength(2, ErrorMessage = "District code cannot be longer than 2 characters.")]
        [Display(Name = "Division")]
        public string DivisionCode { get; set; }
        //[ForeignKey("Division")]
        //public virtual Division Division { get; set; }

        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}