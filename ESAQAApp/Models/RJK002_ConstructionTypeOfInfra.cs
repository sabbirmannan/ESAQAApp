using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_ConstructionTypeOfInfra //ক্ষতিগ্রস্ত স্থাপনার/ মালকিানার ধরণ
    {
        [Key]
        public int ConstructionTypeOfInfraID { get; set; }
        public string ConstructionTypeOfInfraNameEn { get; set; }
        public string ConstructionTypeOfInfraNameBn { get; set; }
    }
}