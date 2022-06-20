using System;
using System.ComponentModel.DataAnnotations;

namespace BAC007.Models
{
    public class Master
    {
        [Key]
        public int MasterDataId { get; set; }

        [StringLength(12)]
        [Display(Name = "কেস নং")]
        public string CaseNo { get; set; }

        [StringLength(10)]
        [Display(Name = "তারিখ")]
        public string FormDate { get; set; }

        [StringLength(100)]
        [Display(Name = "সাক্ষাৎকার গ্রহণের স্থান")]
        public string PlaceOfInterview { get; set; }

        [Display(Name = "জিপিএস লোকেশন (ডিগ্রী)")]
        public int? LocLatDeg { get; set; }

        [Display(Name = "জিপিএস লোকেশন (মিনিট)")]
        public int? LocLatMin { get; set; }

        [Display(Name = "জিপিএস লোকেশন (সেকেন্ড)")]
        public decimal? LocLatSec { get; set; }

        [Display(Name = "জিপিএস লোকেশন")]
        public decimal? LocLatDecimal { get; set; }

        [Display(Name = "জিপিএস লোকেশন (ডিগ্রী)")]
        public int? LocLongDeg { get; set; }

        [Display(Name = "জিপিএস লোকেশন (মিনিট)")]
        public int? LocLongMin { get; set; }

        [Display(Name = "জিপিএস লোকেশন (সেকেন্ড)")]
        public decimal? LocLongSec { get; set; }

        [Display(Name = "জিপিএস লোকেশন")]
        public decimal? LocLongDecimal { get; set; }

        [StringLength(10)]
        [Display(Name = "উত্তরদাতার ধরণ (টিক চিহ্ন দিন)")]
        public string TypeOfRespondent { get; set; }

        [Required]
        [Display(Name = "বিভাগ")]
        [StringLength(2)]
        public string DivisionCode { get; set; }

        [Required]
        [Display(Name = "জেলা")]
        [StringLength(4)]
        public string DistrictCode { get; set; }

        [Required]
        [Display(Name = "উপজেলা")]
        [StringLength(6)]
        public string UpazilaCode { get; set; }

        [Required]
        [Display(Name = "ইউনিয়ন")]
        [StringLength(8)]
        public string UnionCode { get; set; }

        [Display(Name = "গ্রাম")]
        [StringLength(250)]
        public string Village { get; set; }

        [Display(Name = "পাড়া")]
        [StringLength(250)]
        public string Para { get; set; }

        [Display(Name = "ওয়ার্ড নং")]
        [StringLength(20)]
        public string WordNo { get; set; }

        [Display(Name = "বাড়ী নং (যদি থাকে)")]
        [StringLength(150)]
        public string HouseNo { get; set; }

        [StringLength(100)]
        [Display(Name = "সাক্ষাৎকার গ্রহণকারীর নাম")]
        public string NameOfInterviewer { get; set; }

        [StringLength(50)]
        [Display(Name = "স্বাক্ষর")]
        public string InterviewerSignature { get; set; }

        [StringLength(100)]
        [Display(Name = "সুপারভাইজার/ কোয়ালিটি কন্ট্রোল অফিসারের নাম")]
        public string NameOfSupervisor { get; set; }

        [StringLength(50)]
        [Display(Name = "স্বাক্ষর")]
        public string SupervisorSignature { get; set; }

        [StringLength(10)]
        [Display(Name = "তারিখ")]
        public string DataDate { get; set; }

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