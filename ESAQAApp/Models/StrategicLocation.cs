using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using DOF003.Helpers;

namespace DOF003.Models
{
    public class StrategicLocation
    {
        [Key]
        public int StrategicLocationID { get; set; }

        public int? SLID { get; set; }

        [Required(ErrorMessage = "Strategic location name is required!")]
        [Display(Name = "StrategicLocationName", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Strategic location name cannot be longer than 250 characters.")]
        public string StrategicLocationName { get; set; }

        [Display(Name = "ShortFormName", ResourceType = typeof(Resources.Resources))]
        [StringLength(15, ErrorMessage = "Strategic location short name cannot be longer than 15 characters.")]
        public string ShortFormName { get; set; }

        [Display(Name = "IsMonitoring", ResourceType = typeof(Resources.Resources))]
        public bool? IsMonitoring { get; set; }

        [ForeignKey("River")]
        [Display(Name = "RiverName", ResourceType = typeof(Resources.Resources))]
        public int? RiverID { get; set; }
        public virtual River River { get; set; }

        [ForeignKey("Division")]
        [Display(Name = "DivisionName", ResourceType = typeof(Resources.Resources))]
        public int? DivisionID { get; set; }
        public virtual Division Division { get; set; }

        [ForeignKey("District")]
        [Display(Name = "DistrictName", ResourceType = typeof(Resources.Resources))]
        public int? DistrictID { get; set; }
        public virtual District District { get; set; }

        [ForeignKey("Upazilla")]
        [Display(Name = "UpazillaName", ResourceType = typeof(Resources.Resources))]
        public int? UpazillaID { get; set; }
        public virtual Upazilla Upazilla { get; set; }

        [Display(Name = "UnionName", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Union name cannot be longer than 250 characters.")]
        public string UnionName { get; set; }

        [Display(Name = "Village", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Village name cannot be longer than 250 characters.")]
        public string Village { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(Resources.Resources))]
        public decimal? Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(Resources.Resources))]
        public decimal? Longitude { get; set; }

        [Display(Name = "DateOfESTD", ResourceType = typeof(Resources.Resources))]
        [StringLength(50, ErrorMessage = "Date of establishment cannot be longer than 50 characters.")]
        public string DateOfESTD { get; set; }

        [Display(Name = "JLNo", ResourceType = typeof(Resources.Resources))]
        [StringLength(20, ErrorMessage = "JL no cannot be longer than 20 characters.")]
        public string JLNo { get; set; }

        [Display(Name = "UseOfWater", ResourceType = typeof(Resources.Resources))]
        public string UseOfWater { get; set; }

        [Display(Name = "PolutionSource", ResourceType = typeof(Resources.Resources))]
        public string PolutionSource { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.Resources))]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters.")]
        public string Status { get; set; }

        [Display(Name = "Remarks", ResourceType = typeof(Resources.Resources))]
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