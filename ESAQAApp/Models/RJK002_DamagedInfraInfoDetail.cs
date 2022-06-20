using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System;

namespace DOF003.Models
{
    public class DOF003_DamagedInfraInfoDetail //ক্ষতিগ্রস্ত অবকাঠামোর তথ্য 
    {
        [Key]
        public int DamagedInfraInfoDetailID { get; set; }
        public Int32 SurveyMasterID { get; set; }
        public int UseOfInfraID { get; set; } //DOF003_UseOfInfra //Roof walls floor

        public int? RoofInfraTypeID { get; set; } //DOF003_InfraType
        public int? WallInfraTypeID { get; set; } //DOF003_InfraType
        public int? FloorInfraTypeID { get; set; } //DOF003_InfraType
        public int? OneWordInfraTypeID { get; set; } //DOF003_InfraType

        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public int? FloorNumber { get; set; }
        public int? MeasurementUnit { get; set; }
        public decimal? Measurement { get; set; }
        public bool? IsFullyDamaged { get; set; }
        public bool? IsPartiallyDamaged { get; set; }
    }
}