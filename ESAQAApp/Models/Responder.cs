using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class Responder
    {
        [Key]
        public int ResponderID { get; set; }

        [ForeignKey("DataCollectionMaster")]
        [Required(ErrorMessage = "Data Collection Master ID is empty!")]
        public Int32 DataCollectionMasterID { get; set; }
        public virtual DataCollectionMaster DataCollectionMaster { get; set; }

        [Required(ErrorMessage = "Responder name is required!")]
        [Display(Name = "ResponderName", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Responder name cannot be longer than 100 characters.")]
        public string ResponderName { get; set; }

        [Display(Name = "Occupation", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Occupation cannot be longer than 100 characters.")]
        public string Occupation { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        [Display(Name = "ContactNumber", ResourceType = typeof(Resources.Resources))]
        [StringLength(50, ErrorMessage = "Contact number cannot be longer than 50 characters.")]
        public string ContactNumber { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Resources))]
        public bool? IsActive { get; set; }

        [Display(Name = "IsDelete", ResourceType = typeof(Resources.Resources))]
        public bool? IsDelete { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Resources))]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy", ResourceType = typeof(Resources.Resources))]
        public string UpdateBy { get; set; }

        [Display(Name = "UpdatedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "DeletedBy", ResourceType = typeof(Resources.Resources))]
        public string DeletedBy { get; set; }

        [Display(Name = "DeletedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? DeletedDate { get; set; }
    }
}