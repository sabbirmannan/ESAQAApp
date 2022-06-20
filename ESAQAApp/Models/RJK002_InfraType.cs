using System.ComponentModel.DataAnnotations;

namespace DOF003.Models
{
    public class DOF003_InfraType //অবকাঠামোর নির্মাণগত ধরণ
    {
        [Key]
        public int InfraTypeID { get; set; }
        public string InfraTypeNameEn { get; set; }
        public string InfraTypeNameBn { get; set; }
    }
}