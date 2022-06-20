using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_InfraUnit //পরিমাপের একক
    {
        [Key]
        public int InfraUnitID { get; set; }
        public string InfraUnitNameEn { get; set; }
        public string InfraUnitNameBn { get; set; }
    }
}