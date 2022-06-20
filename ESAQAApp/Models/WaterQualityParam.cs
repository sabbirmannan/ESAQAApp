using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class WaterQualityParam
    {
        [Key]
        public int WaterQualityParamID { get; set; }

        [Required(ErrorMessage = "Parameter name is required!")]
        [Display(Name = "ParameterName", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Parameter name cannot be longer than 100 characters.")]
        public string ParameterName { get; set; }

        [ForeignKey("WaterQualityParaFormType")]
        public int? WaterQualityParaFormTypeID { get; set; }
        public virtual WaterQualityParaFormType WaterQualityParaFormType { get; set; }

        [ForeignKey("WaterQualityCategory")]
        public int WaterQualityCategoryID { get; set; }
        public virtual WaterQualityCategory WaterQualityCategory { get; set; }

        [Display(Name = "Unit", ResourceType = typeof(Resources.Resources))]
        [StringLength(25, ErrorMessage = "Unit cannot be longer than 25 characters.")]
        public string Unit { get; set; }

        //Drinking Water
        public bool? IsRange { get; set; }
        public decimal? MinRange { get; set; }
        public decimal? MaxRange { get; set; }
        public decimal? FixedVal { get; set; }

        //Water use for Industries
        public bool? IsRangeIndustry { get; set; }
        public decimal? MinRangeIndustry { get; set; }
        public decimal? MaxRangeIndustry { get; set; }
        public decimal? FixedValIndustry { get; set; }
        
        //Water use for Agriculture/Irrigation
        public bool? IsRangeAgriculture { get; set; }
        public decimal? MinRangeAgriculture { get; set; }
        public decimal? MaxRangeAgriculture { get; set; }
        public decimal? FixedValAgriculture { get; set; }

        //Water use of Fisheries
        public bool? IsRangeFisheries { get; set; }
        public decimal? MinRangeFisheries { get; set; }
        public decimal? MaxRangeFisheries { get; set; }
        public decimal? FixedValFisheries { get; set; }

        //[Required(ErrorMessage = "Standard range is required!")]
        //[Display(Name = "StandardRange", ResourceType = typeof(Resources.Resources))]
        //[StringLength(50, ErrorMessage = "Standard range cannot be longer than 50 characters.")]
        //public string StandardRange { get; set; }

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