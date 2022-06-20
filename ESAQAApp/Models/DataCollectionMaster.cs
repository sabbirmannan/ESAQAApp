using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DataCollectionMaster
    {
        [Key]
        public Int32 DataCollectionMasterID { get; set; }

        [Required(ErrorMessage = "Custom ID  is required!")]
        [Display(Name = "CustomID", ResourceType = typeof(Resources.Resources))]
        [StringLength(25, ErrorMessage = "Custom ID cannot be longer than 25 characters.")]
        public string CustomID { get; set; }

        [ForeignKey("StrategicLocation")]
        [Display(Name = "StrategicLocationName", ResourceType = typeof(Resources.Resources))]
        public int? StrategicLocationID { get; set; }
        public virtual StrategicLocation StrategicLocation { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(Resources.Resources))]
        public decimal? Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(Resources.Resources))]
        public decimal? Longitude { get; set; }

        [StringLength(15, ErrorMessage = "String length cannot be longer than 15 characters.")]
        [Display(Name = "LatitudeDMS", ResourceType = typeof(Resources.Resources))]
        public string LatitudeDMS { get; set; }

        [StringLength(15, ErrorMessage = "String length cannot be longer than 15 characters.")]
        [Display(Name = "LongitudeDMS", ResourceType = typeof(Resources.Resources))]
        public string LongitudeDMS { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, ErrorMessage = "Date cannot be longer than 10 characters.")]
        [Display(Name = "SurveyDate", ResourceType = typeof(Resources.Resources))]
        public string SurveyDate { get; set; }

        [StringLength(50, ErrorMessage = "Lab code cannot be longer than 50 characters.")]
        [Display(Name = "LabCode", ResourceType = typeof(Resources.Resources))]
        public string LabCode { get; set; }

        [ForeignKey("PurposeOfSample")]
        [Display(Name = "PurposeOfSample", ResourceType = typeof(Resources.Resources))]
        public int? PurposeOfSampleID { get; set; }
        public virtual PurposeOfSample PurposeOfSample { get; set; }

        [ForeignKey("WaterQualityCategory")]
        [Display(Name = "TypeOfSample", ResourceType = typeof(Resources.Resources))]
        public int? TypeOfSampleID { get; set; }
        public virtual WaterQualityCategory WaterQualityCategory { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [StringLength(25, ErrorMessage = "Date cannot be longer than 25 characters.")]
        [Display(Name = "SampleCollectionDateTime", ResourceType = typeof(Resources.Resources))]
        public string SampleCollectionDateTime { get; set; }

        [StringLength(100, ErrorMessage = "Sample received by cannot be longer than 100 characters.")]
        [Display(Name = "SampleReceivedBy", ResourceType = typeof(Resources.Resources))]
        public string SampleReceivedBy { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [StringLength(25, ErrorMessage = "Date cannot be longer than 25 characters.")]
        [Display(Name = "ReceivingDateTimeAtLab", ResourceType = typeof(Resources.Resources))]
        public string ReceivingDateTimeAtLab { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, ErrorMessage = "Date cannot be longer than 10 characters.")]
        [Display(Name = "CompletionDate", ResourceType = typeof(Resources.Resources))]
        public string CompletionDate { get; set; }

        [StringLength(50, ErrorMessage = "Lab code cannot be longer than 50 characters.")]
        [Display(Name = "WeatherCondition", ResourceType = typeof(Resources.Resources))]
        public string WeatherCondition { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool? IsActive { get; set; }

        [Display(Name = "IsDelete", ResourceType = typeof(Resources.Resources))]
        public bool? IsDelete { get; set; }

        [StringLength(100, ErrorMessage = "Date cannot be longer than 10 characters.")]
        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }
        
        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? CreatedDate { get; set; }

        [StringLength(100, ErrorMessage = "Date cannot be longer than 10 characters.")]
        [Display(Name = "UpdatedBy", ResourceType = typeof(Resources.Resources))]
        public string UpdateBy { get; set; }

        [Display(Name = "UpdatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(100, ErrorMessage = "Date cannot be longer than 10 characters.")]
        [Display(Name = "DeletedBy", ResourceType = typeof(Resources.Resources))]
        public string DeletedBy { get; set; }

        [Display(Name = "DeletedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? DeletedDate { get; set; }
    }
}