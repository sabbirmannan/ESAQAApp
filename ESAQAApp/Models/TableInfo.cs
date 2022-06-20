using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BAC007.Models
{
    public class TableInfo
    {
        [Key]
        public int TableInfoId { get; set; }

        [Display(Name = "Form Order")]
        public int FormOrder { get; set; }

        [Display(Name = "Which Part?")]
        public int WhichPart { get; set; }

        [Display(Name = "Form Title")]
        public string FormTitle { get; set; }

        [Display(Name = "Table Title")]
        public string TableTitle { get; set; }

        [Display(Name = "Lookup Table Name")]
        public string LookupTableName { get; set; }

        [Display(Name = "Data Save Table Name")]
        public string DataSaveTableName { get; set; }

        [Display(Name = "Is Individual Table?")]
        public bool? IsIndividualTable { get; set; }

        [Display(Name = "Is Response Cols?")]
        public bool? IsResponseCols { get; set; }

        [Display(Name = "Is Fiscal Year Flat?")]
        public bool? IsFiscalYearFlat { get; set; }

        [Display(Name = "Is Fiscal Year Two Cols?")]
        public bool? IsFiscalYearTwoCols { get; set; }
    }
}