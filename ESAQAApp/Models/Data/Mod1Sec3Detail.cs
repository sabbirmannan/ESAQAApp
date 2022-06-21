using System;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Models
{
    public class Mod1Sec3Detail
    {
        [Key]
        public int Mod1Sec3DetailId { get; set; }

        public int MasterDataId { get; set; }

        public int? IsMemberOfAnyNgo { get; set; } //1.21

        public string TrainingOrgs { get; set; } //1.22, comma seperated
        public string OtherTrainingOrgName { get; set; } //1.22 other
        public int? TrainingTotalYear { get; set; } //1.23 1
        public int? TrainingTotalMonth { get; set; } //1.23 2

        public int? HasNgoLoan { get; set; } //1.24
        public int? HasTrainingFromNgoGov { get; set; } //1.25
        public int? HasTrainingOnAgri { get; set; } //1.26

        public string TrainingAgris { get; set; } //1.27, comma seperated
        public string OtherTrainingAgriName { get; set; } //1.27 other

        public int? IsTrainingNeedOnAgri { get; set; } //1.28

        public string TrainingNeedAgris { get; set; } //1.29, comma seperated
        public string OtherTrainingNeedAgriName { get; set; } //1.29 other

        public int? AnyMemberOfWaterMng { get; set; } //1.30
    }
}