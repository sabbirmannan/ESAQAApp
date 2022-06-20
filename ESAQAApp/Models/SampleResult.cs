using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class SampleResult
    {
        [Key]
        public int SampleResultID { get; set; }

        [ForeignKey("DataCollectionMaster")]
        [Required(ErrorMessage = "Data Collection Master ID is empty!")]
        public Int32 DataCollectionMasterID { get; set; }
        public virtual DataCollectionMaster DataCollectionMaster { get; set; }

        //[ForeignKey("WaterQualityParam")]
        //[Required(ErrorMessage = "Please select a water quality parameter!")]
        //public int WaterQualityParamID { get; set; }
        //public virtual WaterQualityParam WaterQualityParam { get; set; }

        [Display(Name = "ParameterName", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Parameter name range cannot be longer than 100 characters.")]
        public string ParameterName { get; set; }

        [Display(Name = "Result", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Result cannot be longer than 150 characters.")]
        public string Result { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(25, ErrorMessage = "Date cannot be longer than 25 characters.")]
        [Display(Name = "CollectionMonth", ResourceType = typeof(Resources.Resources))]
        public string CollectionMonth { get; set; }

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