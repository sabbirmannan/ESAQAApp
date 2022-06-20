using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using BAC007.Models;
using System.Web.Mvc;

namespace BAC007.Controllers
{
    public class CommonAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GetDistrict
        //public SelectList GetDistrict(int DivisionID)
        //{
        //    SelectList origList = new();// SelectList(db.District.Where(w => w.DivisionID == DivisionID && w.IsActive == true && w.IsDelete == false).OrderBy(o => o.DistrictName), "DistrictID", "DistrictName", "");

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
