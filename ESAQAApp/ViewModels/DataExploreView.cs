using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.ViewModels
{
    public class DataExploreView
    {
        public Int32 DataCollectionMasterID { get; set; }

        [Display(Name = "CustomID", ResourceType = typeof(Resources.Resources))]
        public string CustomID { get; set; }

        [Display(Name = "UpazillaName", ResourceType = typeof(Resources.Resources))]
        public string UpazillaName { get; set; }

        [Display(Name = "RiverName", ResourceType = typeof(Resources.Resources))]
        public string RiverName { get; set; }

        [Display(Name = "SamplingLocation", ResourceType = typeof(Resources.Resources))]
        public string SamplingLocation { get; set; }

        [Display(Name = "SampleResultCount", ResourceType = typeof(Resources.Resources))]
        public int SampleResultCount { get; set; }

        [Display(Name = "SurveyDate", ResourceType = typeof(Resources.Resources))]
        public string SurveyDate { get; set; }

        [Display(Name = "SLID", ResourceType = typeof(Resources.Resources))]
        public string SLID { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(Resources.Resources))]
        public string Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(Resources.Resources))]
        public string Longitude { get; set; }
    }

    public class StrategicLocationList
    {
        [Key]
        public int StrategicLocationID { get; set; }
        public int Serial { get; set; }
        public string SLID { get; set; }
        public string Location { get; set; }
        public string District { get; set; }
        public string Upazilla { get; set; }
        public string Union { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string DateOfESTD { get; set; }
        public string UseOfWater { get; set; }
        public string PolutionSource { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}