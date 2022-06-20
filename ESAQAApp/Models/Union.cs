using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Union
    {
        [Key]
        [StringLength(8, ErrorMessage = "Union code cannot be longer than 8 characters.")]
        public string UnionCode { get; set; }

        [Required(ErrorMessage = "Union name is required!")]
        [Display(Name = "Union")]
        [StringLength(150, ErrorMessage = "Union name cannot be longer than 150 characters.")]
        public string UnionName { get; set; }

        [Required(ErrorMessage = "Upazila is required!")]
        [StringLength(6, ErrorMessage = "Upazila code cannot be longer than 6 characters.")]
        [Display(Name = "Upazila")]
        public string UpazilaCode { get; set; }
        //[ForeignKey("Upazila")]
        //public virtual Upazila Upazila { get; set; }

        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}