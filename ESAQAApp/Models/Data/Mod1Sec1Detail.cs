using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec1Detail
    {
        [Key]
        public int Mod1Sec1DetailId { get; set; }

        public int MasterDataId { get; set; }

        [StringLength(250)]
        //[Display(Name = "উত্তরদাতা/ উত্তরদাত্রীর নাম")]
        //[StringLength(2, ErrorMessage = "District code cannot be longer than 2 characters.")]
        public string NameOfRespondent { get; set; }

        [StringLength(11)]
        //[Display(Name = "মোবাইল")]        
        public string MobileNo { get; set; }

        //[Display(Name = "বয়স")]
        public int? RespondentAge { get; set; }

        public int? EducationLevelId { get; set; }

        public int? MaritalStatusId { get; set; }

        public int? IsRespondentFamilyMaster { get; set; }

        public int? HouseTotalMale { get; set; }
        public int? HouseTotalFemale { get; set; }

        public int? IsAgriMainOccupation { get; set; }

        public int? HouseAgriTotalMale { get; set; }
        public int? HouseAgriTotalFemale { get; set; }

        public int? HouseEduTotalMale { get; set; }
        public int? HouseEduTotalFemale { get; set; }
    }
}