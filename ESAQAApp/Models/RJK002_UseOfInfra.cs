using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_UseOfInfra //অবকাঠামোর ব্যবহার
    {
        [Key]
        public int UseOfInfraID { get; set; }
        public string UseOfInfraNameEn { get; set; }
        public string UseOfInfraNameBn { get; set; }
    }
}