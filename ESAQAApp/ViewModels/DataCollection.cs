using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOF003.Models;
using Microsoft.AspNet.Identity;

namespace DOF003.ViewModels
{
    public class DataCollection
    {
        [Key]
        public int DataCollectionID { get; set; }
        public string UserName { get; set; }

        public DataCollection()
        {
            
        }

        public virtual DataCollectionMaster DataCollectionMaster { get; set; }
        //public IEnumerable<DataCollectionMaster> DataCollectionMasters { get; set; }
        //public virtual DataCollectionDetail DataCollectionDetail { get; set; }
        //public IEnumerable<DataCollectionDetail> DataCollectionDetails { get; set; }
        //public virtual Responder Responder { get; set; }
        //public IEnumerable<Responder> Responders { get; set; }
        //public virtual Interviewer Interviewer { get; set; }
        //public IEnumerable<Interviewer> Interviewers { get; set; }
        //public virtual ResponsiblePerson ResponsiblePerson { get; set; }
       // public IEnumerable<ResponsiblePerson> ResponsiblePersons { get; set; }
        public virtual SampleResult SampleResult { get; set; }
        public IEnumerable<SampleResult> SampleResults { get; set; }

        public virtual HydrologicalInformation HydrologicalInformation { get; set; }
    }
}