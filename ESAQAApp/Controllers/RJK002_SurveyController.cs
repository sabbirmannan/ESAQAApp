using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using Microsoft.AspNet.Identity;
using DOF003.Helpers;
using DOF003.Models;

namespace DOF003.Controllers
{
    public class DOF003_SurveyController : BaseController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private CommonHelper _ch = new CommonHelper();
        private readonly CommonController _cc = new CommonController();
        private readonly ExecuteClass exec = new ExecuteClass();
        private string msg = string.Empty;
        private Notification result = new Notification();

        // GET: DOF003_Survey
        public ActionResult Index()
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(EntryUser))
            {
                return RedirectToAction("Login", "Account");
            }

            if (EntryUser == "rony" || EntryUser == "admin")
            {
                return View(db.DOF003_SurveyMaster.Where(w => w.IsDelete == false).ToList());
            }
            else
            {
                return View(db.DOF003_SurveyMaster.Where(w => w.EntryUser == EntryUser && w.IsDelete == false).ToList());
            }
        }

        // GET: DOF003_Survey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOF003_SurveyMaster DOF003_SurveyMaster = db.DOF003_SurveyMaster.Find(id);
            if (DOF003_SurveyMaster == null)
            {
                return HttpNotFound();
            }
            return View(DOF003_SurveyMaster);
        }

        // GET: DOF003_Survey/Create
        public ActionResult Create()
        {
            string EntryUser = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(EntryUser))
            {
                //TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ConstructionTypeOfInfra = db.DOF003_ConstructionTypeOfInfra.ToList();
            ViewBag.TypeOfDamagedInfra = db.DOF003_TypeOfDamagedInfra.ToList();
            ViewBag.UseOfInfra = db.DOF003_UseOfInfra.ToList();
            ViewBag.InfraType = db.DOF003_InfraType.ToList();
            ViewBag.InfraUnit = db.DOF003_InfraUnit.ToList();
            ViewBag.TypeOfTree = db.DOF003_TypeOfTree.ToList();

            return View();
        }

        // POST: DOF003_Survey/SaveMaster
        [HttpPost]
        public async Task<ActionResult> SaveMaster(DOF003_SurveyMaster master)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (master.SurveyMasterID == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    master.Latitude = master.Latitude ?? 0;
                    master.Longitude = master.Longitude ?? 0;
                    master.EntryUser = EntryUser;
                    master.EntryDate = DateTime.Now;
                    master.IsDelete = false;

                    db.DOF003_SurveyMaster.Add(master);
                    x = await db.SaveChangesAsync() > 0 ? true : false;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = master.SurveyMasterID,
                            status = "success",
                            message = "Data has been saved."
                        };
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        string error = "Data not saved and has been rollbacked!";

                        result = new Notification
                        {
                            id = master.SurveyMasterID,
                            status = "error",
                            message = "",
                            exception = error
                        };
                    }
                }
                #endregion
            }
            else
            {
                string query = $@"UPDATE [dbo].[DOF003_SurveyMaster]
                                SET [SurveyDate] = '{master.SurveyDate}', 
                                [AffectedPerson] = '{master.AffectedPerson}', 
                                [DateOfBirth] = '{master.DateOfBirth}', 
                                [GuardianName] = '{master.GuardianName}', 
                                [Mobile] = '{master.Mobile}', 
                                [Profession] = '{master.Profession}', 
                                [PersonNid] = '{master.PersonNid}', 
                                [Latitude] = {master.Latitude ?? 0}, 
                                [Longitude] = {master.Longitude ?? 0}, 
                                [UpdateUser] = '{EntryUser}', 
                                [UpdateDate] = GETDATE() 
                                WHERE [SurveyMasterID] = {master.SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = master.SurveyMasterID,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = master.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/Part2Info
        [HttpPost]
        public ActionResult Part2Info(Part2Info part2)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (part2.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = part2.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"UPDATE [dbo].[DOF003_SurveyMaster]
                                SET [HoldingNo] = '{part2.HoldingNo}', 
                                [RoadNo] = '{part2.RoadNo}', 
                                [SectorNo] = '{part2.SectorNo}', 
                                [Union] = '{part2.Union}', 
                                [Upazila] = '{part2.Upazila}', 
                                [District] = '{part2.District}', 
                                [AffectedPersonType] = {part2.AffectedPersonType ?? 0}, 
                                [AffectedPersonTypeOther] = '{part2.AffectedPersonTypeOther}', 
                                [UpdateUser] = '{EntryUser}', 
                                [UpdateDate] = GETDATE() 
                                WHERE [SurveyMasterID] = {part2.SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = part2.SurveyMasterID,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = part2.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/Part3Info
        [HttpPost]
        public ActionResult Part3Info(Part3Info part3)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (part3.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = part3.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"UPDATE [dbo].[DOF003_SurveyMaster]
                                SET [TypeOfDamagedInfraList] = '{part3.TypeOfDamagedInfraList}', 
                                [TypeOfDamagedInfraOther] = '{part3.TypeOfDamagedInfraOther}', 
                                [UpdateUser] = '{EntryUser}', 
                                [UpdateDate] = GETDATE() 
                                WHERE [SurveyMasterID] = {part3.SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = part3.SurveyMasterID,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = part3.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/Part3Info
        [HttpPost]
        public ActionResult Part4Info(Part4Info part4)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (part4.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = part4.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"UPDATE [dbo].[DOF003_SurveyMaster]
                                SET [IsTreeAffected] = '{part4.IsTreeAffected}', 
                                [UpdateUser] = '{EntryUser}', 
                                [UpdateDate] = GETDATE() 
                                WHERE [SurveyMasterID] = {part4.SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = part4.SurveyMasterID,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = part4.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/Part3Info
        [HttpPost]
        public ActionResult Part5Info(Part5Info part5)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (part5.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = part5.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"UPDATE [dbo].[DOF003_SurveyMaster]
                                SET [AffectedPlotLatitude] = {part5.AffectedPlotLatitude ?? 0}, 
                                [AffectedPlotLongitude] = {part5.AffectedPlotLongitude ?? 0}, 
                                [PlotNo] = '{part5.PlotNo}', 
                                [PlotArea] = {part5.PlotArea ?? 0}, 
                                [PlotSectorNo] = '{part5.PlotSectorNo}', 
                                [PlotUnion] = '{part5.PlotUnion}', 
                                [PlotUpazila] = '{part5.PlotUpazila}', 
                                [PlotDistrict] = '{part5.PlotDistrict}', 
                                [PlotOwnerName] = '{part5.PlotOwnerName}', 
                                [PlotOwnerContact] = '{part5.PlotOwnerContact}', 
                                [UpdateUser] = '{EntryUser}', 
                                [UpdateDate] = GETDATE() 
                                WHERE [SurveyMasterID] = {part5.SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = part5.SurveyMasterID,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = part5.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Save Damaged Infra Info
        // POST: DOF003_Survey/SaveDamagedInfraInfo
        [HttpPost]
        public ActionResult SaveDamagedInfraInfo(DamagedInfraInfoDetailTemp diid)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (diid.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = diid.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    string query = $@"INSERT INTO [dbo].[DOF003_DamagedInfraInfoDetail]([SurveyMasterID], [UseOfInfraID], [RoofInfraTypeID], [WallInfraTypeID], [FloorInfraTypeID], [OneWordInfraTypeID], [Length], [Width], [Height], [FloorNumber], [MeasurementUnit], [IsFullyDamaged], [IsPartiallyDamaged]) 
                                      VALUES({diid.SurveyMasterID}, {diid.UseOfInfraID}, {diid.RoofInfraTypeID}, {diid.WallInfraTypeID}, {diid.FloorInfraTypeID}, {diid.OneWordInfraTypeID}, {diid.Length}, {diid.Width}, {diid.Height}, {diid.FloorNumber}, {diid.MeasurementUnit}, {diid.IsFullyDamaged}, {diid.IsPartiallyDamaged})";

                    x = exec.NonSelectQuery(query, ref msg);

                    if (x)
                    {
                        result = new Notification
                        {
                            id = diid.SurveyMasterID,
                            status = "success",
                            message = "Data has been saved."
                        };
                    }
                    else
                    {
                        string error = $"Data not saved!<br />{msg}";

                        result = new Notification
                        {
                            id = diid.SurveyMasterID,
                            status = "error",
                            message = "",
                            exception = error
                        };
                    }
                }
                catch (Exception ex)
                {
                    string error = ExtractInnerException(ex);

                    result = new Notification
                    {
                        id = diid.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/GetDamagedInfraInfo
        [HttpGet]
        public ActionResult GetDamagedInfraInfo(int SurveyMasterID)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            List<DamagedInfraInfoDetailTemp> diidList = new List<DamagedInfraInfoDetailTemp>();

            if (SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    string query = $@"SELECT d.DamagedInfraInfoDetailID, d.SurveyMasterID,                                    
                                    d.UseOfInfraID, 
                                    dbo.DOF003_UseOfInfra.UseOfInfraNameBn, 
                                    --d.RoofInfraTypeID, 
                                    RoofInfraType.InfraTypeNameBn RoofInfraTypeID, 
                                    --d.WallInfraTypeID, 
                                    WallInfraType.InfraTypeNameBn WallInfraTypeID, 
                                    --d.FloorInfraTypeID, 
                                    FloorInfraType.InfraTypeNameBn FloorInfraTypeID, 
                                    --d.OneWordInfraTypeID, 
                                    OneWordInfraType.InfraTypeNameBn OneWordInfraTypeID, 
                                    d.Length, d.Width, d.Height, d.FloorNumber, 
                                    d.MeasurementUnit, dbo.DOF003_InfraUnit.InfraUnitNameBn, d.Measurement, 
                                    CASE WHEN d.IsFullyDamaged = 1 THEN 1 ELSE 0 END AS IsFullyDamaged, 
                                    CASE WHEN d.IsPartiallyDamaged = 1 THEN 1 ELSE 0 END AS IsPartiallyDamaged
                                    FROM dbo.DOF003_DamagedInfraInfoDetail AS d 
                                    LEFT JOIN dbo.DOF003_UseOfInfra ON d.UseOfInfraID = dbo.DOF003_UseOfInfra.UseOfInfraID 
                                    LEFT JOIN dbo.DOF003_InfraUnit ON d.MeasurementUnit = dbo.DOF003_InfraUnit.InfraUnitID 
                                    LEFT JOIN dbo.DOF003_InfraType AS RoofInfraType ON d.RoofInfraTypeID = RoofInfraType.InfraTypeID 
                                    LEFT JOIN dbo.DOF003_InfraType AS WallInfraType ON d.WallInfraTypeID = WallInfraType.InfraTypeID 
                                    LEFT JOIN dbo.DOF003_InfraType AS FloorInfraType ON d.FloorInfraTypeID = FloorInfraType.InfraTypeID 
                                    LEFT JOIN dbo.DOF003_InfraType AS OneWordInfraType ON d.OneWordInfraTypeID = OneWordInfraType.InfraTypeID 
                                    WHERE d.SurveyMasterID = {SurveyMasterID}";

                    DataTable dtResult = exec.SelectQuery(query, ref msg);

                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtResult.Rows)
                        {
                            DamagedInfraInfoDetailTemp diid = new DamagedInfraInfoDetailTemp
                            {
                                DamagedInfraInfoDetailID = Convert.ToInt32(row["DamagedInfraInfoDetailID"].ToString()),
                                SurveyMasterID = Convert.ToInt32(row["SurveyMasterID"].ToString()),
                                UseOfInfraID = Convert.ToInt32(row["UseOfInfraID"].ToString()),
                                UseOfInfraNameBn = Convert.ToString(row["UseOfInfraNameBn"].ToString()),
                                RoofInfraTypeID = Convert.ToString(row["RoofInfraTypeID"].ToString()),
                                WallInfraTypeID = Convert.ToString(row["WallInfraTypeID"].ToString()),
                                FloorInfraTypeID = Convert.ToString(row["FloorInfraTypeID"].ToString()),
                                OneWordInfraTypeID = Convert.ToString(row["OneWordInfraTypeID"].ToString()),
                                Length = Convert.ToString(row["Length"].ToString()),
                                Width = Convert.ToString(row["Width"].ToString()),
                                Height = Convert.ToString(row["Height"].ToString()),
                                FloorNumber = Convert.ToString(row["FloorNumber"].ToString()),
                                MeasurementUnit = Convert.ToString(row["MeasurementUnit"].ToString()),
                                Measurement = Convert.ToString(row["Measurement"].ToString()),
                                IsFullyDamaged = Convert.ToInt32(row["IsFullyDamaged"].ToString()),
                                IsPartiallyDamaged = Convert.ToInt32(row["IsPartiallyDamaged"].ToString())
                            };

                            diidList.Add(diid);
                        }

                        return Json(diidList, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = new Notification
                        {
                            id = SurveyMasterID,
                            status = "warning",
                            message = "Sorry, no data found!",
                            exception = ""
                        };
                    }
                }
                catch (Exception ex)
                {
                    string error = ExtractInnerException(ex);

                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/DeleteDamagedInfraInfo
        [HttpPost]
        public ActionResult DeleteDamagedInfraInfo(int DamagedInfraInfoDetailID, int SurveyMasterID)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"DELETE FROM [dbo].[DOF003_DamagedInfraInfoDetail] 
                                  WHERE DamagedInfraInfoDetailID = {DamagedInfraInfoDetailID} AND SurveyMasterID = {SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "success",
                        message = "Data has been deleted."
                    };
                }
                else
                {
                    string error = $"Data not deleted!<br />{msg}";

                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Damaged Tree Info
        // POST: DOF003_Survey/SaveDamagedTreeInfo
        [HttpPost]
        public ActionResult SaveDamagedTreeInfo(DamagedTreeInfoDetailTemp dtid)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (dtid.SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = dtid.SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    string query = $@"INSERT INTO [dbo].[DOF003_DamagedTreeInfoDetail]([SurveyMasterID], [TreeName], [TypeOfTreeID], [BigTree], [MediumTree], [SmallTree], [SaplingTree], [IsOwnerTree], [IsSocialTree]) 
                                      VALUES({dtid.SurveyMasterID}, '{dtid.TreeName}', {dtid.TypeOfTreeID}, {dtid.BigTree}, {dtid.MediumTree}, {dtid.SmallTree}, {dtid.SaplingTree}, {dtid.IsOwnerTree}, {dtid.IsSocialTree})";

                    x = exec.NonSelectQuery(query, ref msg);

                    if (x)
                    {
                        result = new Notification
                        {
                            id = dtid.SurveyMasterID,
                            status = "success",
                            message = "Data has been saved."
                        };
                    }
                    else
                    {
                        string error = $"Data not saved!<br />{msg}";

                        result = new Notification
                        {
                            id = dtid.SurveyMasterID,
                            status = "error",
                            message = "",
                            exception = error
                        };
                    }
                }
                catch (Exception ex)
                {
                    string error = ExtractInnerException(ex);

                    result = new Notification
                    {
                        id = dtid.SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/GetDamagedTreeInfo
        [HttpGet]
        public ActionResult GetDamagedTreeInfo(int SurveyMasterID)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            List<DamagedTreeInfoDetailTemp> dtidList = new List<DamagedTreeInfoDetailTemp>();

            if (SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    string query = $@"SELECT d.DamagedTreeInfoDetailID, d.SurveyMasterID, d.TreeName, 
                                    d.TypeOfTreeID, dbo.DOF003_TypeOfTree.TypeOfTreeNameBn, 
                                    d.BigTree, d.MediumTree, d.SmallTree, d.SaplingTree,
                                    CASE WHEN d.IsOwnerTree = 1 THEN 1 ELSE 0 END AS IsOwnerTree, 
                                    CASE WHEN d.IsSocialTree = 1 THEN 1 ELSE 0 END AS IsSocialTree
                                    FROM dbo.DOF003_DamagedTreeInfoDetail AS d 
                                    LEFT JOIN dbo.DOF003_TypeOfTree ON d.TypeOfTreeID = dbo.DOF003_TypeOfTree.TypeOfTreeID
                                    WHERE d.SurveyMasterID = {SurveyMasterID}";

                    DataTable dtResult = exec.SelectQuery(query, ref msg);

                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtResult.Rows)
                        {
                            DamagedTreeInfoDetailTemp dtid = new DamagedTreeInfoDetailTemp
                            {
                                DamagedTreeInfoDetailID = Convert.ToInt32(row["DamagedTreeInfoDetailID"].ToString()),
                                SurveyMasterID = Convert.ToInt32(row["SurveyMasterID"].ToString()),
                                TreeName = Convert.ToString(row["TreeName"].ToString()),
                                TypeOfTreeID = Convert.ToString(row["TypeOfTreeID"].ToString()),
                                TypeOfTreeNameBn = Convert.ToString(row["TypeOfTreeNameBn"].ToString()),
                                BigTree = Convert.ToString(row["BigTree"].ToString()),
                                MediumTree = Convert.ToString(row["MediumTree"].ToString()),
                                SmallTree = Convert.ToString(row["SmallTree"].ToString()),
                                SaplingTree = Convert.ToString(row["SaplingTree"].ToString()),
                                IsOwnerTree = Convert.ToInt32(row["IsOwnerTree"].ToString()),
                                IsSocialTree = Convert.ToInt32(row["IsSocialTree"].ToString())
                            };

                            dtidList.Add(dtid);
                        }

                        return Json(dtidList, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = new Notification
                        {
                            id = SurveyMasterID,
                            status = "warning",
                            message = "Sorry, no data found!",
                            exception = ""
                        };
                    }
                }
                catch (Exception ex)
                {
                    string error = ExtractInnerException(ex);

                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: DOF003_Survey/DeleteDamagedTreeInfo
        [HttpPost]
        public ActionResult DeleteDamagedTreeInfo(int DamagedTreeInfoDetailID, int SurveyMasterID)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (SurveyMasterID == 0)
            {
                result = new Notification
                {
                    id = SurveyMasterID,
                    status = "error",
                    message = "",
                    exception = "দয়া করে ক্ষতিগ্রস্থ ব্যক্তির পরিচয় দিয়ে আসুন এবং পরে আবার চেষ্টা করুন!"
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string query = $@"DELETE FROM [dbo].[DOF003_DamagedTreeInfoDetail] 
                                  WHERE DamagedTreeInfoDetailID = {DamagedTreeInfoDetailID} AND SurveyMasterID = {SurveyMasterID}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "success",
                        message = "Data has been deleted."
                    };
                }
                else
                {
                    string error = $"Data not deleted!<br />{msg}";

                    result = new Notification
                    {
                        id = SurveyMasterID,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        // GET: DOF003_Survey/Edit
        public ActionResult Edit(int? id)
        {
            DOF003_SurveyMaster master = new DOF003_SurveyMaster();
            string EntryUser = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(EntryUser))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != null)
            {
                master = db.DOF003_SurveyMaster.Find(id);
            }
            else
            {
                TempData["Message"] = ShowMessage(Sign.Danger, "Sorry, no data found!");
                return RedirectToAction("Index", "DOF003_Survey");
            }

            ViewBag.ConstructionTypeOfInfra = db.DOF003_ConstructionTypeOfInfra.ToList();
            ViewBag.TypeOfDamagedInfra = db.DOF003_TypeOfDamagedInfra.ToList();
            ViewBag.UseOfInfra = db.DOF003_UseOfInfra.ToList();
            ViewBag.InfraType = db.DOF003_InfraType.ToList();
            ViewBag.InfraUnit = db.DOF003_InfraUnit.ToList();
            ViewBag.TypeOfTree = db.DOF003_TypeOfTree.ToList();

            return View(master);
        }

        // GET: /DOF003_Survey/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DOF003_SurveyMaster master = await db.DOF003_SurveyMaster.FindAsync(id);

            if (master == null)
            {
                return HttpNotFound();
            }

            return View(master);
        }

        // POST: /DOF003_Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DOF003_SurveyMaster master = await db.DOF003_SurveyMaster.FindAsync(id);

            if (master != null)
            {
                master.IsDelete = true;
                master.DeleteUser = User.Identity.GetUserName();
                master.DeleteDate = DateTime.Now;

                try
                {
                    db.Entry(master).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    string message = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = ShowMessage(Sign.Danger, message);
                    return View(master);
                }
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
