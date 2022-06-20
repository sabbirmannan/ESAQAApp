using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_TypeOfTreeSize
    {
        [Key]
        public int TypeOfTreeSizeID { get; set; }

        public string TypeOfTreeSizeNameEn { get; set; }
        public string TypeOfTreeSizeNameBn { get; set; }
    }
}