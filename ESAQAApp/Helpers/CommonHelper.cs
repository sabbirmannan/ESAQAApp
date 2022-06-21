using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using ESAQAApp.Models;
//using ESAQAApp.ViewModels;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;

namespace ESAQAApp.Helpers
{
    public class CommonHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CommonHelper()
        {

        }

        //public SelectList GetWaterQualityParams()
        //{
        //    SelectList origList = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false), "WaterQualityParamID", "ParameterName", "");

        //    List<SelectListItem> newList = origList.ToList();
        //    newList.Insert(0, new SelectListItem() { Value = "", Text = "Select an option..." });

        //    var selectedItem = newList.FirstOrDefault(item => item.Selected);
        //    var selectedItemValue = String.Empty;
        //    if (selectedItem != null)
        //    {
        //        selectedItemValue = selectedItem.Value;
        //    }

        //    return new SelectList(newList, "Value", "Text", selectedItemValue);
        //}
    }    
}