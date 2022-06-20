using System;
using System.ComponentModel.DataAnnotations;

namespace DOF003.Models
{
    public class River
    {
        [Key]
        public int RiverID { get; set; }

        //[Required(ErrorMessage = "River ID of bengali standard is required!")]
        [Display(Name = "RiverStandardID", ResourceType = typeof(Resources.Resources))]
        [StringLength(20, ErrorMessage = "River ID of bengali standard cannot be longer than 20 characters.")]
        public string RiverStandardID { get; set; }

        [Required(ErrorMessage = "River name is required!")]
        [Display(Name = "RiverName", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "River name cannot be longer than 250 characters.")]
        public string RiverName { get; set; }

        [Display(Name = "RiverNameInBangla", ResourceType = typeof(Resources.Resources))]
        [StringLength(500, ErrorMessage = "Name of bengali cannot be longer than 500 characters.")]
        public string RiverNameInBangla { get; set; }

        [Display(Name = "Location", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Location name cannot be longer than 250 characters.")]
        public string Location { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool? IsActive { get; set; }

        [Display(Name = "IsDelete", ResourceType = typeof(Resources.Resources))]
        public bool? IsDelete { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy", ResourceType = typeof(Resources.Resources))]
        public string UpdateBy { get; set; }

        [Display(Name = "UpdatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "DeletedBy", ResourceType = typeof(Resources.Resources))]
        public string DeletedBy { get; set; }

        [Display(Name = "DeletedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? DeletedDate { get; set; }
    }
}