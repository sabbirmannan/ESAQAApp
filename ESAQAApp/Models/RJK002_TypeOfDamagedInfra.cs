using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_TypeOfDamagedInfra //ক্ষতিগ্রস্ত অবকাঠামোর ধরণ
    {
        [Key]
        public int TypeOfDamagedInfraID { get; set; }
        public string TypeOfDamagedInfraNameEn { get; set; }
        public string TypeOfDamagedInfraNameBn { get; set; }
    }
}