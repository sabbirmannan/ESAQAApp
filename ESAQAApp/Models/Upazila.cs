using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Upazila
    {
        [Key]
        [StringLength(6, ErrorMessage = "Upazila code cannot be longer than 6 characters.")]
        public string UpazilaCode { get; set; }

        [Required(ErrorMessage = "Upazila name is required!")]
        [Display(Name = "Upazila")]
        [StringLength(100, ErrorMessage = "Upazila name cannot be longer than 100 characters.")]
        public string UpazilaName { get; set; }

        [Required(ErrorMessage = "District is required!")]
        [StringLength(4, ErrorMessage = "District code cannot be longer than 4 characters.")]
        [Display(Name = "District")]
        public string DistrictCode { get; set; }
        //[ForeignKey("District")]
        //public virtual District District { get; set; }        

        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}