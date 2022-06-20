using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class LocationImage
    {
        [Key]
        public int LocationImageID { get; set; }

        [ForeignKey("DataCollectionMaster")]
        [Required(ErrorMessage = "Data Collection Master ID is empty!")]
        public Int32 DataCollectionMasterID { get; set; }
        public virtual DataCollectionMaster DataCollectionMaster { get; set; }

        public string ImageName { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public string UploadBy { get; set; }
    }
}