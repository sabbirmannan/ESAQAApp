using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Division
    {
        [Key]
        [StringLength(2, ErrorMessage = "Division code cannot be longer than 2 characters.")]
        public string DivisionCode { get; set; }

        [Required(ErrorMessage = "Division name is required!")]
        [Display(Name = "Division")]
        [StringLength(50, ErrorMessage = "Division name cannot be longer than 50 characters.")]
        public string DivisionName { get; set; }

        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}