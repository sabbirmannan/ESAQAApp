using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DOF003.Models
{
    public class ResponsiblePerson
    {
        [Key]
        public int ResponsiblePersonID { get; set; }

        [ForeignKey("DataCollectionMaster")]
        [Required(ErrorMessage = "Data Collection Master ID is empty!")]
        public Int32 DataCollectionMasterID { get; set; }
        public virtual DataCollectionMaster DataCollectionMaster { get; set; }

        [Required(ErrorMessage = "Responsible person name is required!")]
        [Display(Name = "ResponsibleName", ResourceType = typeof(Resources.Resources))]
        [StringLength(100, ErrorMessage = "Responsible person name cannot be longer than 100 characters.")]
        public string ResponsibleName { get; set; }

        [Display(Name = "Locality", ResourceType = typeof(Resources.Resources))]
        [StringLength(150, ErrorMessage = "Locality cannot be longer than 150 characters.")]
        public string Locality { get; set; }

        [Required(ErrorMessage = "Sampling location name is required!")]
        [Display(Name = "SamplingLocation", ResourceType = typeof(Resources.Resources))]
        [StringLength(250, ErrorMessage = "Sampling location name cannot be longer than 250 characters.")]
        public string SamplingLocation { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CollectionDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? CollectionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "TestingDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? TestingDate { get; set; }

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