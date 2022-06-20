using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF003.Models
{
    public class DOF003_SurveyMaster
    {
        [Key]
        [Display(Name = "আইডি")]
        public Int32 SurveyMasterID { get; set; }

        [Display(Name = "জরিপের তারিখ")]
        public string SurveyDate { get; set; }

        [Display(Name = "ক্ষতিগ্রস্ত ব্যক্তি/ প্রতষ্ঠিান/ স্থাপনার নাম")]
        public string AffectedPerson { get; set; }

        [Display(Name = "জন্ম তারিখ")]
        public string DateOfBirth { get; set; }

        [Display(Name = "ক্ষতিগ্রস্ত ব্যক্তির বাবা/ স্বামীর নাম")]
        public string GuardianName { get; set; }

        [Display(Name = "মোবাইল নং")]
        public string Mobile { get; set; }

        [Display(Name = "পেশা")]
        public string Profession { get; set; }

        [Display(Name = "জাতীয় পরিচিয় পত্রের নাম্বার")]
        public string PersonNid { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00######}", NullDisplayText = "")]
        [Display(Name = "ক্ষতিগ্রস্থ অবকাঠামোর জিপিএস কো-অর্ডিনেট (অক্ষাংশ-Latitude)")]
        public decimal? Latitude { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00######}", NullDisplayText = "")]
        [Display(Name = "ক্ষতিগ্রস্থ অবকাঠামোর জিপিএস কো-অর্ডিনেট (দ্রাঘিমাংশ-Longitude)")]
        public decimal? Longitude { get; set; }

        [Display(Name = "হোল্ডিং নং")]
        public string HoldingNo { get; set; }

        [Display(Name = "রোডের নং")]
        public string RoadNo { get; set; }

        [Display(Name = "সেক্টর নং")]
        public string SectorNo { get; set; }

        [Display(Name = "ইউনিয়ন")]
        public string Union { get; set; }

        [Display(Name = "উপজেলা")]
        public string Upazila { get; set; }

        [Display(Name = "জেলা")]
        public string District { get; set; }

        [Display(Name = "ক্ষতিগ্রস্ত স্থাপনার/ মালিকানার ধরণ")]
        public int? AffectedPersonType { get; set; }

        [Display(Name = "ক্ষতিগ্রস্ত স্থাপনার/ মালিকানার ধরণ (অন্যান্য)")]
        public string AffectedPersonTypeOther { get; set; }

        [Display(Name = "অবকাঠামোর তথ্য (আবাসিক, বাণিজ্যিক, সামাজিক ও ধর্র্মীয় প্রতিষ্ঠান এবং অন্যান্য)")]
        public string TypeOfDamagedInfraList { get; set; } //[DOF003_TypeOfDamagedInfra], multile by comma seperated

        [Display(Name = "অন্যান্য")]
        public string TypeOfDamagedInfraOther { get; set; }

        [Display(Name = "অন্যান্য")]
        public bool? IsTreeAffected { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00######}", NullDisplayText = "")]
        [Display(Name = "অক্ষাংশ-Latitude")]
        public decimal? AffectedPlotLatitude { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00######}", NullDisplayText = "")]
        [Display(Name = "দ্রাঘিমাংশ-Longitude")]
        public decimal? AffectedPlotLongitude { get; set; }

        [Display(Name = "প্লট নং")]
        public string PlotNo { get; set; }

        [Display(Name = "জমির পরিমান (প্লটে জমির পরিমাপ)")]
        public decimal? PlotArea { get; set; }

        [Display(Name = "সেক্টর নং")]
        public string PlotSectorNo { get; set; }

        [Display(Name = "ইউনিয়ন")]
        public string PlotUnion { get; set; }

        [Display(Name = "উপজেলা")]
        public string PlotUpazila { get; set; }

        [Display(Name = "জেলা")]
        public string PlotDistrict { get; set; }

        [Display(Name = "প্লট মালিকের নাম")]
        public string PlotOwnerName { get; set; }

        [Display(Name = "মোবাইল নং")]
        public string PlotOwnerContact { get; set; }

        [Display(Name = "এন্ট্রি ইউজার")]
        public string EntryUser { get; set; }

        [Display(Name = "এন্ট্রি তারিখ")]
        public DateTime? EntryDate { get; set; }

        [Display(Name = "আপডেট ইউজার")]
        public string UpdateUser { get; set; }

        [Display(Name = "আপডেট তারিখ")]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "ডিলিট ইউজার")]
        public string DeleteUser { get; set; }

        [Display(Name = "ডিলিট তারিখ")]
        public DateTime? DeleteDate { get; set; }

        public bool? IsDelete { get; set; }
    }

    public class Part2Info
    {
        public Int32 SurveyMasterID { get; set; }
        public string HoldingNo { get; set; }
        public string RoadNo { get; set; }
        public string SectorNo { get; set; }
        public string Union { get; set; }
        public string Upazila { get; set; }
        public string District { get; set; }
        public int? AffectedPersonType { get; set; }
        public string AffectedPersonTypeOther { get; set; }
    }

    public class Part3Info
    {
        public Int32 SurveyMasterID { get; set; }
        public string TypeOfDamagedInfraList { get; set; }
        public string TypeOfDamagedInfraOther { get; set; }
    }

    public class Part4Info
    {
        public Int32 SurveyMasterID { get; set; }
        public int? IsTreeAffected { get; set; }
    }

    public class Part5Info
    {
        public Int32 SurveyMasterID { get; set; }
        public decimal? AffectedPlotLatitude { get; set; }
        public decimal? AffectedPlotLongitude { get; set; }
        public string PlotNo { get; set; }
        public decimal? PlotArea { get; set; }
        public string PlotSectorNo { get; set; }
        public string PlotUnion { get; set; }
        public string PlotUpazila { get; set; }
        public string PlotDistrict { get; set; }
        public string PlotOwnerName { get; set; }
        public string PlotOwnerContact { get; set; }
    }

    public class DamagedInfraInfoDetailTemp //ক্ষতিগ্রস্ত অবকাঠামোর তথ্য 
    {
        public Int32 DamagedInfraInfoDetailID { get; set; }
        public Int32 SurveyMasterID { get; set; }
        public int UseOfInfraID { get; set; } //DOF003_UseOfInfra //Roof walls floor
        public string UseOfInfraNameBn { get; set; }
        public string RoofInfraTypeID { get; set; } //DOF003_InfraType
        public string WallInfraTypeID { get; set; } //DOF003_InfraType
        public string FloorInfraTypeID { get; set; } //DOF003_InfraType
        public string OneWordInfraTypeID { get; set; } //DOF003_InfraType
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FloorNumber { get; set; }
        public string MeasurementUnit { get; set; }
        public string Measurement { get; set; }
        public int? IsFullyDamaged { get; set; }
        public int? IsPartiallyDamaged { get; set; }
    }

    public class DamagedTreeInfoDetailTemp
    {
        public int DamagedTreeInfoDetailID { get; set; }
        public Int32 SurveyMasterID { get; set; }
        public string TreeName { get; set; }
        public string TypeOfTreeID { get; set; }
        public string TypeOfTreeNameBn { get; set; }
        public string BigTree { get; set; }
        public string MediumTree { get; set; }
        public string SmallTree { get; set; }
        public string SaplingTree { get; set; }
        public int? IsOwnerTree { get; set; }
        public int? IsSocialTree { get; set; }
    }
}