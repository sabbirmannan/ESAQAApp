using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class WaterQualityCategory
    {
        [Key]
        public int WaterQualityCategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required!")]
        [Display(Name = "CategoryName", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Category name cannot be longer than 100 characters.")]
        public string CategoryName { get; set; }

        [Display(Name = "Symbol", ResourceType = typeof(Resources.Resources))]
        [StringLength(5, ErrorMessage = "Symbol cannot be longer than 5 characters.")]
        public string Symbol { get; set; }

        [Display(Name = "StandardRange", ResourceType = typeof(Resources.Resources))]
        [StringLength(50, ErrorMessage = "Standard range cannot be longer than 50 characters.")]
        public string StandardRange { get; set; }

        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Remarks cannot be longer than 250 characters.")]
        public string Remarks { get; set; }

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