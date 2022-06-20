using System.ComponentModel.DataAnnotations;
using System;

namespace DOF003.Models
{
    public class DOF003_DamagedTreeInfoDetail
    {
        [Key]
        public int DamagedTreeInfoDetailID { get; set; }
        public Int32 SurveyMasterID { get; set; }
        public string TreeName { get; set; }
        public int? TypeOfTreeID { get; set; } //DOF003_TypeOfTree
        public int? BigTree { get; set; }
        public int? MediumTree { get; set; }
        public int? SmallTree { get; set; }
        public int? SaplingTree { get; set; }
        public bool? IsOwnerTree { get; set; }
        public bool? IsSocialTree { get; set; }
    }
}