using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class DOF003_TypeOfTree
    {
        [Key]
        public int TypeOfTreeID { get; set; }

        public string TypeOfTreeNameEn { get; set; }
        public string TypeOfTreeNameBn { get; set; }
    }
}