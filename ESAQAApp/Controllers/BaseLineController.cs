using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BAC007.Helpers;
using BAC007.Models;
using BAC007.Models.Data;
using BAC007.Models.Temp;
using DAL;
using Microsoft.AspNet.Identity;
using PagedList;
using BAC007.Models.Temp;

namespace BAC007.Controllers
{
    public class BaseLineController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ExecuteClass exec = new ExecuteClass();
        private string msg = string.Empty;
        private Notification result = new Notification();

        // GET: BaseLine
        public ActionResult Index(int page = 1, int pageSize = 15)
        {
            if (page == 1)
                ViewBag.StartSerailNo = page;
            else
                ViewBag.StartSerailNo = ((page - 1) * pageSize) + 1;

            List<GetMasterDataList> _list = new List<GetMasterDataList>();

            string query = @"SELECT dbo.Masters.MasterDataId, dbo.Masters.CaseNo, 
                            dbo.Masters.FormDate, dbo.Masters.PlaceOfInterview, 
                            dbo.Masters.LocLatDecimal, dbo.Masters.LocLongDecimal, 
                            dbo.Masters.TypeOfRespondent, dbo.Divisions.DivisionName, 
                            dbo.Districts.DistrictName, dbo.Upazilas.UpazilaName, 
                            dbo.Unions.UnionName, dbo.Masters.Village, dbo.Masters.Para, 
                            dbo.Masters.WordNo, dbo.Masters.HouseNo, 
                            dbo.Masters.NameOfInterviewer, dbo.Masters.NameOfSupervisor, 
                            dbo.Masters.IsActive, dbo.Masters.IsDelete,
                            dbo.Masters.CreatedBy, dbo.Masters.CreatedDate
                            FROM dbo.Masters 
                            LEFT JOIN dbo.Unions ON dbo.Masters.UnionCode = dbo.Unions.UnionCode 
                            LEFT JOIN dbo.Upazilas ON dbo.Masters.UpazilaCode = dbo.Upazilas.UpazilaCode 
                            LEFT JOIN dbo.Districts ON dbo.Masters.DistrictCode = dbo.Districts.DistrictCode 
                            LEFT JOIN dbo.Divisions ON dbo.Masters.DivisionCode = dbo.Divisions.DivisionCode
                            WHERE dbo.Masters.IsActive = 1 AND dbo.Masters.IsDelete = 0";

            DataTable dtResult = exec.SelectQuery(query, ref msg);

            if (dtResult.Rows.Count > 0)
            {
                _list = dtResult.DataTableToList<GetMasterDataList>();
            }
            else
            {
                _list = new List<GetMasterDataList>();
            }

            var pl = new PagedList<GetMasterDataList>(_list, page, pageSize);
            return View(pl);
        }

        // GET: BaseLine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: BaseLine/Create
        public ActionResult Create()
        {
            string EntryUser = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(EntryUser))
            {
                //TempData["Message"] = ShowMessage(Sign.Success, OperationMessage.Delete.ToDescription());
                return RedirectToAction("Login", "Account");
            }

            ViewBag.DivisionCode = new SelectList(db.Division.OrderBy(o => o.SortingOrder), "DivisionCode", "DivisionName");
            ViewBag.EducationLevelId = db.LookupEducationLevel.ToList();
            ViewBag.MaritalStatusId = db.LookupMaritalStatus.ToList();
            ViewBag.MovablePropertyOption = db.LookupMovablePropertyOption.ToList();

            ViewBag.FurnitureOtherMaterialOption = db.LookupFurnitureOther111.ToList();
            ViewBag.AgriNonAgriAssetOption = db.LookupAgriNonagriAsset112.ToList();
            ViewBag.GrossHouseholdIncOption = db.LookupGrossHouseholdIncome115.ToList();
            ViewBag.FoodConsExpenseOption = db.LookupFoodConsumpExps116.ToList();
            ViewBag.StmOfExpenditureOption = db.LookupStatementOfExpenditure117.ToList();
            ViewBag.LoanSource119 = db.LookupLoanSource119.ToList();
            ViewBag.UseOfLoanCode119 = db.LookupUseOfLoanCode119.ToList();

            ViewBag.TrainingOrganization122 = db.LookupTrainingOrganization122.ToList();
            ViewBag.AgriTrainingList127 = db.LookupAgriTrainingList127n129.ToList();
            ViewBag.AgriTrainingList129 = db.LookupAgriTrainingList127n129.ToList();

            ViewBag.HouseStructureType131 = db.LookupHouseStructureType131.ToList();
            ViewBag.DrinkingWaterSource133 = db.LookupDrinkingWaterSource133.ToList();
            ViewBag.SanitaryType135 = db.LookupSanitaryType135.ToList();

            ViewBag.SickList136 = db.LookupSickList136.ToList();
            ViewBag.TreatmentPlace136 = db.LookupTreatmentPlace136.ToList();
            ViewBag.WorkForLivingJobType137 = db.LookupWorkForLivingJobType137.ToList();

            ViewBag.HouseholdAgriLand21 = db.LookupMod2HouseholdAgriLand21.ToList();
            ViewBag.HouseholdAgriLandType22 = db.LookupMod2HouseholdAgriLandType22.ToList();

            ViewBag.HouseholdLandType = db.LookupMod2HouseholdLandType.ToList();
            ViewBag.HouseholdCropCode = db.LookupMod2HouseholdCropCode.ToList();

            ViewBag.WaterSourceCode = db.LookupMod2WaterSourceCode.ToList();
            ViewBag.IrrigationSysCode = db.LookupMod2IrrigationSysCode.ToList();
            ViewBag.AvailabilityCode = db.LookupMod2AvailabilityCode.ToList();
            ViewBag.CropProcessingCode = db.LookupMod2CropProcessing.ToList();
            ViewBag.CropMarketingCode = db.LookupMod2CropMarketing.ToList();
            ViewBag.ProdDamageCodeId = db.LookupMod2ProdDamageCode.ToList();
            ViewBag.ProdDamageReasonCodeId = db.LookupMod2ProdDamageReasonCode.ToList();
            ViewBag.Mod3TypeOfChanges31Id = db.LookupMod3TypeOfChanges31.ToList();
            ViewBag.Mod3ImpactOfSubProject31Id = db.LookupMod3ImpactOfSubProject31.ToList();
            ViewBag.Mod3DrySeaWaterMngLandType33Id = db.LookupMod3DrySeaWaterMngLandType33.ToList();
            ViewBag.FuelCode35Id = db.LookupMod3FuelCode35.ToList();
            ViewBag.Mod3IrrigatedAgriLandType35Id = db.LookupMod3IrrigatedAgriLandType35.ToList();

            ViewBag.Mod3NaturalDisaster32Id = db.LookupMod3NaturalDisaster32.ToList();
            ViewBag.Mod3Recurrence32Id = db.LookupMod3Recurrence32.ToList();
            ViewBag.Mod3Extension32Id = db.LookupMod3Extension32.ToList();
            ViewBag.Mod3Dimension32Id = db.LookupMod3Dimension32.ToList();

            ViewBag.HouseholdWaterUse36Id = db.LookupMod3HouseholdWaterUse36.ToList();
            ViewBag.HouseholdWaterSource36Id = db.LookupMod3HouseholdWaterSource36.ToList();
            ViewBag.HouseholdOwnershipCode36Id = db.LookupMod3HouseholdOwnershipCode36.ToList();
            ViewBag.HouseholdWaterProperties36Id = db.LookupMod3HouseholdWaterProperties36.ToList();
            ViewBag.HouseholdWaterArsenic36Id = db.LookupMod3HouseholdWaterArsenic36.ToList();
            ViewBag.CurrentStatusSubProj38Id = db.LookupMod3CurrentStatusSubProj38.ToList();
            ViewBag.CurrentStatus38Id = db.LookupMod3CurrentStatus38.ToList();

            return View();
        }

        // POST: BaseLine/SaveMaster
        [HttpPost]
        public async Task<ActionResult> SaveMaster(Master master)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (master.MasterDataId == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    string CaseNo = master.CaseNo.Split('_')[0].ToString();
                    int MaxCase = db.Master.Where(w => w.CaseNo.StartsWith(CaseNo)).Count() + 1;
                    CaseNo = $"{CaseNo}_{MaxCase.ToString().PadLeft(3, '0')}";

                    master.CaseNo = CaseNo;
                    master.CreatedBy = EntryUser;
                    master.CreatedDate = DateTime.Now;
                    master.IsActive = true;
                    master.IsDelete = false;

                    db.Master.Add(master);
                    x = await db.SaveChangesAsync() > 0;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = master.MasterDataId,
                            caseno = master.CaseNo,
                            status = "success",
                            message = "Data has been saved. Case #" + master.CaseNo
                        };
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        string error = "Data not saved and has been rollbacked!";

                        result = new Notification
                        {
                            id = master.MasterDataId,
                            caseno = master.CaseNo,
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
                string query = $@"UPDATE [dbo].[Masters]
                                SET [CaseNo] = '{master.CaseNo.Trim()}',
                                [FormDate] = '{master.FormDate.Trim()}',
                                [PlaceOfInterview] = N'{master.PlaceOfInterview.Trim()}',
                                [LocLatDeg] = {master.LocLatDeg ?? 0}, 
                                [LocLatMin] = {master.LocLatMin ?? 0}, 
                                [LocLatSec] = {master.LocLatSec ?? 0}, 
                                [LocLatDecimal] = {master.LocLatDecimal ?? 0}, 
                                [LocLongDeg] = {master.LocLongDeg ?? 0}, 
                                [LocLongMin] = {master.LocLongMin ?? 0}, 
                                [LocLongSec] = {master.LocLongSec ?? 0}, 
                                [LocLongDecimal] = {master.LocLongDecimal ?? 0}, 
                                [TypeOfRespondent] = '{master.TypeOfRespondent.Trim()}',
                                [DivisionCode] = '{master.DivisionCode.Trim()}',
                                [DistrictCode] = '{master.DistrictCode.Trim()}',
                                [UpazilaCode] = '{master.UpazilaCode.Trim()}',
                                [UnionCode] = '{master.UnionCode.Trim()}',
                                [Village] = N'{master.Village.Trim()}',
                                [Para] = N'{master.Para.Trim()}',
                                [WordNo] = N'{master.WordNo.Trim()}',
                                [HouseNo] = N'{master.HouseNo.Trim()}',
                                [NameOfInterviewer] = N'{master.NameOfInterviewer.Trim()}',
                                [NameOfSupervisor] = N'{master.NameOfSupervisor.Trim()}',
                                [DataDate] = '',
                                [UpdateBy] = '{EntryUser}',
                                [UpdatedDate] = GETDATE()
                                WHERE [MasterDataId] = {master.MasterDataId}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = master.MasterDataId,
                        caseno = master.CaseNo,
                        status = "success",
                        message = "Data has been updated. Case #" + master.CaseNo
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = master.MasterDataId,
                        caseno = master.CaseNo,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region ১.১০ বর্তমানে আপনার খানার অস্থাবর সম্পত্তির পরিমাণ সংখ্যায় উল্লেখ করুন
        // POST: BaseLine/SaveMod1Sec1Detail
        [HttpPost]
        public async Task<ActionResult> SaveMod1Sec1Detail(Mod1Sec1Detail mod1Sec1Detail)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (mod1Sec1Detail.Mod1Sec1DetailId == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    db.Mod1Sec1Detail.Add(mod1Sec1Detail);
                    x = await db.SaveChangesAsync() > 0 ? true : false;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = mod1Sec1Detail.Mod1Sec1DetailId,
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
                            id = mod1Sec1Detail.Mod1Sec1DetailId,
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
                string query = $@"UPDATE [dbo].[Mod1Sec1Detail]
                                SET 
                                [NameOfRespondent] = '{mod1Sec1Detail.NameOfRespondent}', 
                                [MobileNo] = '{mod1Sec1Detail.MobileNo}', 
                                [RespondentAge] = {mod1Sec1Detail.RespondentAge ?? 0}, 
                                [EducationLevelId] = {mod1Sec1Detail.EducationLevelId ?? 0}, 
                                [MaritalStatusId] = {mod1Sec1Detail.MaritalStatusId ?? 0}, 
                                [IsRespondentFamilyMaster] = {mod1Sec1Detail.IsRespondentFamilyMaster ?? 0}, 
                                [HouseTotalMale] = {mod1Sec1Detail.HouseTotalMale ?? 0}, 
                                [HouseTotalFemale] = {mod1Sec1Detail.HouseTotalFemale ?? 0}, 
                                [IsAgriMainOccupation] = {mod1Sec1Detail.IsAgriMainOccupation ?? 0}, 
                                [HouseAgriTotalMale] = {mod1Sec1Detail.HouseAgriTotalMale ?? 0}, 
                                [HouseAgriTotalFemale] = {mod1Sec1Detail.HouseAgriTotalFemale ?? 0}, 
                                [HouseEduTotalMale] = {mod1Sec1Detail.HouseEduTotalMale ?? 0}, 
                                [HouseEduTotalFemale] = {mod1Sec1Detail.HouseEduTotalFemale ?? 0}
                                WHERE [MasterDataId] = {mod1Sec1Detail.MasterDataId}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = mod1Sec1Detail.Mod1Sec1DetailId,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = mod1Sec1Detail.Mod1Sec1DetailId,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: BaseLine/SaveMovablePropertyTable
        [HttpPost]
        public ActionResult SaveMovablePropertyTable(Mod1Sec1Table110 data)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            try
            {
                data.LandDecimal = data.LandDecimal ?? 0;
                data.LandPresentValue = data.LandPresentValue ?? 0;
                db.Mod1Sec1Table110.Add(data);
                x = db.SaveChanges() > 0;

                //string query = $@"INSERT INTO [dbo].[Mod1Sec1Table110]([MasterDataId], [MovablePropertyOptionId], [Others], [LandDecimal], [LandPresentValue]) 
                //                  VALUES({data.MasterDataId}, {data.MovablePropertyOptionId}, N'{data.Others}', {data.LandDecimal ?? 0}, {data.LandPresentValue ?? 0})";

                //x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification()
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification()
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification()
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occured!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // HttpGet: BaseLine/GetMovablePropertyInfo
        [HttpGet]
        public ActionResult GetMovablePropertyInfo(int master_id)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            List<GetMod1Sec1Table110DataTemp> list = new List<GetMod1Sec1Table110DataTemp>();

            try
            {
                string query = $@"SELECT d.Mod1Sec1Table110Id, d.MasterDataId, 
                                d.MovablePropertyOptionId, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.LandDecimal, d.LandPresentValue
                                FROM dbo.Mod1Sec1Table110 AS d 
                                LEFT JOIN dbo.LookupMovablePropertyOptions AS o ON d.MovablePropertyOptionId = o.OptionID
                                WHERE d.MasterDataId = {master_id} 
                                ORDER BY d.MovablePropertyOptionId";

                DataTable dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GetMod1Sec1Table110DataTemp rd = new GetMod1Sec1Table110DataTemp()
                        {
                            RowId = dr["Mod1Sec1Table110Id"].ToString().ToInt32(),
                            MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                            MovablePropertyOptionId = dr["MovablePropertyOptionId"].ToString().ToInt32(),
                            OptionName = dr["OptionName"].ToString(),
                            Others = dr["Others"].ToString(),
                            LandDecimal = dr["LandDecimal"].ToString().ToDecimal(),
                            LandPresentValue = dr["LandPresentValue"].ToString().ToDecimal()
                        };

                        list.Add(rd);
                    }
                }
                else
                {
                    list = new List<GetMod1Sec1Table110DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod1Sec1Table110DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // HttpGet: BaseLine/DeleteResponseDataInfo
        [HttpPost]
        public ActionResult DeleteMovablePropertyInfo(string row_id, string table_name)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            try
            {
                string query = $@"DELETE FROM [dbo].[{table_name}] WHERE [{table_name}Id] = {row_id}";
                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification()
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification()
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification()
                {
                    id = 0,
                    status = "error",
                    message = "An error occured!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১১  আসবাবপত্র ও অন্যান্য সামগ্রি (পরিবারের মালিকানাধীন : বর্তমানে) 
        [HttpGet]
        public ActionResult GetFurnitureOtherMaterial(int masterId)
        {
            var list = new List<GetMod1Sec1Table111DataTemp>();
            try
            {
                var query = $@"SELECT d.Mod1Sec1Table111Id, d.MasterDataId, 
                                d.FurnitureOther111Id, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.ItemNumber, d.ItemPresentValue
                                FROM dbo.Mod1Sec1Table111 AS d 
                                LEFT JOIN dbo.LookupFurnitureOther111 AS o ON d.FurnitureOther111Id = o.LookupFurnitureOther111Id
                                WHERE d.MasterDataId = {masterId} 
                                ORDER BY d.FurnitureOther111Id";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec1Table111DataTemp
                                  {
                                      RowId = dr["Mod1Sec1Table111Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      FurnitureOtherMaterialOptionId = dr["FurnitureOther111Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      ItemNumber = dr["ItemNumber"].ToString().ToDecimal(),
                                      ItemPresentValue = dr["ItemPresentValue"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec1Table111DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec1Table111DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveFurnitureOtherMaterial(Mod1Sec1Table111 data)
        {
            try
            {
                data.ItemNumber = data.ItemNumber ?? 0;
                data.ItemPresentValue = data.ItemPresentValue ?? 0;
                db.Mod1Sec1Table111.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFurnitureOtherMaterial(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১২ আপনার খানার কৃষিকাজ  ও অ-কৃষিকাজ  সম্পত্তির পরিমাণ সংখ্যায় উল্লেখ করুন
        [HttpGet]
        public ActionResult GetAgriNonAgriAsset(int masterId)
        {
            var list = new List<GetMod1Sec1Table112DataTemp>();
            try
            {
                var query = $@"SELECT d.Mod1Sec1Table112Id, d.MasterDataId, 
                                d.AgriNonagriAsset112Id, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.ItemNumber, d.ItemPresentValue
                                FROM dbo.Mod1Sec1Table112 AS d 
                                LEFT JOIN dbo.LookupAgriNonagriAsset112 AS o ON d.AgriNonagriAsset112Id = o.AgriNonagriAsset112Id
                                WHERE d.MasterDataId = {masterId} 
                                ORDER BY d.AgriNonagriAsset112Id";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec1Table112DataTemp
                                  {
                                      RowId = dr["Mod1Sec1Table112Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      AgriNonAgriAssetOtherOptionId = dr["AgriNonagriAsset112Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      ItemNumber = dr["ItemNumber"].ToString().ToDecimal(),
                                      ItemPresentValue = dr["ItemPresentValue"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec1Table112DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec1Table112DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAgriNonAgriAsset(Mod1Sec1Table112 data)
        {
            try
            {
                data.ItemNumber = data.ItemNumber ?? 0;
                data.ItemPresentValue = data.ItemPresentValue ?? 0;
                db.Mod1Sec1Table112.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAgriNonAgriAsset(string rowId, string tableName)
        {
            try
            {
                string query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১৫ নিম্নোক্ত ঘরে খানার সামগ্রীক আয়ের ববরন দিন (মাসিক-বাৎসরিক হলে সেটা মাসে কনভার্ট করতে হবে) 
        [HttpGet]
        public ActionResult GetGrossHouseholdIncome(int masterId)
        {
            var list = new List<GetMod1Sec1Table115DataTemp>();
            try
            {
                var query = $@"SELECT d.Mod1Sec1Table115Id, d.MasterDataId, 
                                d.GrossHouseholdIncome115Id, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.MonthlyIncome, d.YearlyIncome
                                FROM dbo.Mod1Sec1Table115 AS d 
                                LEFT JOIN dbo.LookupGrossHouseholdIncome115 AS o ON d.GrossHouseholdIncome115Id = o.GrossHouseholdIncome115Id
                                WHERE d.MasterDataId = {masterId} 
                                ORDER BY d.GrossHouseholdIncome115Id";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec1Table115DataTemp
                                  {
                                      RowId = dr["Mod1Sec1Table115Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      GrossHouseholdOtherIncomeId = dr["GrossHouseholdIncome115Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      MonthlyIncome = dr["MonthlyIncome"].ToString().ToDecimal(),
                                      YearlyIncome = dr["YearlyIncome"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec1Table115DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec1Table115DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveGrossHouseholdIncome(Mod1Sec1Table115 data)
        {
            try
            {
                data.MonthlyIncome = data.MonthlyIncome ?? 0;
                data.YearlyIncome = data.YearlyIncome ?? 0;
                db.Mod1Sec1Table115.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteGrossHouseholdIncome(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১৬: খানার ভোগ ব্যয়ের বিবরন দিন 
        [HttpGet]
        public ActionResult GetFoodConsExpense(int masterId)
        {
            var list = new List<GetMod1Sec1Table116DataTemp>();
            try
            {
                var query = $@"SELECT d.Mod1Sec1Table116Id, d.MasterDataId, 
                                d.FoodConsumpExps116Id, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.AmountOfConsumption, d.ConsumptionValue, d.MonthlyAmount, d.TotalPrice
                                FROM dbo.Mod1Sec1Table116 AS d 
                                LEFT JOIN dbo.LookupFoodConsumpExps116 AS o ON d.FoodConsumpExps116Id = o.FoodConsumpExps116Id
                                WHERE d.MasterDataId = {masterId} 
                                ORDER BY d.FoodConsumpExps116Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec1Table116DataTemp
                                  {
                                      RowId = dr["Mod1Sec1Table116Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      FoodConsExpOtherOptionId = dr["FoodConsumpExps116Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      AmountOfConsumption = dr["AmountOfConsumption"].ToString().ToDecimal(),
                                      ConsumptionValue = dr["ConsumptionValue"].ToString().ToDecimal(),
                                      MonthlyAmount = dr["MonthlyAmount"].ToString().ToDecimal(),
                                      TotalPrice = dr["TotalPrice"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec1Table116DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec1Table116DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveFoodConsExpense(Mod1Sec1Table116 data)
        {
            try
            {
                data.AmountOfConsumption = data.AmountOfConsumption ?? 0;
                data.ConsumptionValue = data.ConsumptionValue ?? 0;
                data.MonthlyAmount = data.MonthlyAmount ?? 0;
                data.TotalPrice = data.TotalPrice ?? 0;
                db.Mod1Sec1Table116.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteFoodConsExpense(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১৭: নিত্য ব্যবহার্য দ্রব্যাদি ও অন্যান্য খাতে ব্যয়ের বিবরন দিন (২০১৯-২০২০)
        [HttpGet]
        public ActionResult GetStmOfExpenditure(int masterId)
        {
            var list = new List<GetMod1Sec1Table117DataTemp>();
            try
            {
                var query = $@"SELECT d.Mod1Sec1Table116Id, d.MasterDataId, 
                            d.StatementOfExpenditure117Id, o.OptionName + ' ' + ISNULL(d.Others, '') AS OptionName, d.Others, d.TotalAmount
                            FROM dbo.Mod1Sec1Table117 AS d 
                            LEFT JOIN dbo.LookupStatementOfExpenditure117 AS o ON d.StatementOfExpenditure117Id = o.StatementOfExpenditure117Id
                            WHERE d.MasterDataId = {masterId} 
                            ORDER BY d.StatementOfExpenditure117Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec1Table117DataTemp
                                  {
                                      RowId = dr["Mod1Sec1Table116Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      StatementOfExpenditureOptionId = dr["StatementOfExpenditure117Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      TotalAmount = dr["TotalAmount"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec1Table117DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec1Table117DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveStmOfExpenditure(Mod1Sec1Table117 data)
        {
            try
            {
                data.TotalAmount = data.TotalAmount ?? 0;
                db.Mod1Sec1Table117.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteStmOfExpenditure(string rowId, string tableName)
        {
            try
            {
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ১.১৯ যদি হ্যাঁ হয়, অনুগ্রহ করে নিচের ছকটি পূরণ করুন
        [HttpGet]
        public ActionResult GetMod1Sec2Table119(int masterId)
        {
            var list = new List<GetMod1Sec2Table119DataTemp>();
            try
            {
                var query = $@"SELECT b.Mod1Sec2Table119Id, b.MasterDataId, b.LoanSource119Id, m1.OptionName + ' ' + ISNULL(b.Others, '') AS OptionName, 
                            b.Others, b.LoanAmount, b.UseOfLoanCode119Id, m2.OptionName AS LoanCodeOptionName, b.InstallmentAmount, b.AvgInterest
                            FROM dbo.Mod1Sec2Table119 AS b 
                            LEFT JOIN dbo.LookupLoanSource119 AS m1 ON b.LoanSource119Id = m1.LoanSource119Id 
                            LEFT JOIN dbo.LookupUseOfLoanCode119 AS m2 ON b.UseOfLoanCode119Id = m2.UseOfLoanCode119Id 
                            WHERE b.MasterDataId = {masterId} 
                            ORDER BY b.LoanSource119Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec2Table119DataTemp
                                  {
                                      RowId = dr["Mod1Sec2Table119Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      LoanSource119Id = dr["LoanSource119Id"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      LoanAmount = dr["LoanAmount"].ToString().ToDecimal(),
                                      UseOfLoanCode119Id = dr["UseOfLoanCode119Id"].ToString().ToInt(),
                                      LoanCodeOptionName = dr["LoanCodeOptionName"].ToString(),
                                      InstallmentAmount = dr["InstallmentAmount"].ToString().ToDecimal(),
                                      AvgInterest = dr["AvgInterest"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec2Table119DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod1Sec2Table119DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod1Sec2Table119(Mod1Sec2Table119 data)
        {
            try
            {
                db.Mod1Sec2Table119.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod1Sec2Table119(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region সেকশন ২ পেশা, আয় ও ব্যয়
        // POST: BaseLine/SaveMod1Sec2Detail
        [HttpPost]
        public async Task<ActionResult> SaveMod1Sec2Detail(Mod1Sec2Detail mod1Sec2Detail)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (mod1Sec2Detail.Mod1Sec2DetailId == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    db.Mod1Sec2Detail.Add(mod1Sec2Detail);
                    x = await db.SaveChangesAsync() > 0 ? true : false;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = mod1Sec2Detail.Mod1Sec2DetailId,
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
                            id = mod1Sec2Detail.Mod1Sec2DetailId,
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
                string query = $@"UPDATE [dbo].[Mod1Sec2Detail]
                                SET 
                                [MainOccupation] = {mod1Sec2Detail.MainOccupation ?? 0}, 
                                [TotalMonthEngage] = {mod1Sec2Detail.TotalMonthEngage ?? 0}, 
                                [HasLastYearLoan] = {mod1Sec2Detail.HasLastYearLoan ?? 0}
                                WHERE [MasterDataId] = {mod1Sec2Detail.MasterDataId}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = mod1Sec2Detail.Mod1Sec2DetailId,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = mod1Sec2Detail.Mod1Sec2DetailId,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region সেকশন ৩ সামাজিক সংগঠনে সম্পৃক্ততা ও প্রশিক্ষণ
        // POST: BaseLine/SaveMod1Sec3Detail
        [HttpPost]
        public async Task<ActionResult> SaveMod1Sec3Detail(Mod1Sec3Detail mod1Sec3Detail)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (mod1Sec3Detail.Mod1Sec3DetailId == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    db.Mod1Sec3Detail.Add(mod1Sec3Detail);
                    x = await db.SaveChangesAsync() > 0 ? true : false;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = mod1Sec3Detail.Mod1Sec3DetailId,
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
                            id = mod1Sec3Detail.Mod1Sec3DetailId,
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
                string query = $@"UPDATE [dbo].[Mod1Sec3Detail]
                                SET 
                                [IsMemberOfAnyNgo] = {mod1Sec3Detail.IsMemberOfAnyNgo ?? 0}, 
                                [TrainingOrgs] = '{mod1Sec3Detail.TrainingOrgs}', 
                                [OtherTrainingOrgName] = N'{mod1Sec3Detail.OtherTrainingOrgName}',
                                [TrainingTotalYear] = {mod1Sec3Detail.TrainingTotalYear ?? 0},
                                [TrainingTotalMonth] = {mod1Sec3Detail.TrainingTotalMonth ?? 0},
                                [HasNgoLoan] = {mod1Sec3Detail.HasNgoLoan ?? 0},
                                [HasTrainingFromNgoGov] = {mod1Sec3Detail.HasTrainingFromNgoGov ?? 0},
                                [HasTrainingOnAgri] = {mod1Sec3Detail.HasTrainingOnAgri ?? 0},
                                [TrainingAgris] = '{mod1Sec3Detail.TrainingAgris}',
                                [OtherTrainingAgriName] = N'{mod1Sec3Detail.OtherTrainingAgriName}',
                                [IsTrainingNeedOnAgri] = {mod1Sec3Detail.IsTrainingNeedOnAgri ?? 0},
                                [TrainingNeedAgris] = '{mod1Sec3Detail.TrainingNeedAgris}',
                                [OtherTrainingNeedAgriName] = N'{mod1Sec3Detail.OtherTrainingNeedAgriName}',
                                [AnyMemberOfWaterMng] = {mod1Sec3Detail.AnyMemberOfWaterMng ?? 0}
                                WHERE [MasterDataId] = {mod1Sec3Detail.MasterDataId}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = mod1Sec3Detail.Mod1Sec3DetailId,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = mod1Sec3Detail.Mod1Sec3DetailId,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region সেকশন ৪ জীবনযাত্রার মান
        // POST: BaseLine/SaveMod1Sec3Detail
        [HttpPost]
        public async Task<ActionResult> SaveMod1Sec4Detail(Mod1Sec4Detail mod1Sec4Detail)
        {
            string EntryUser = User.Identity.GetUserName();
            string EntryUserID = User.Identity.GetUserId();
            bool x = false;

            if (mod1Sec4Detail.Mod1Sec4DetailId == 0)
            {
                #region New Data :: Save
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    db.Mod1Sec4Detail.Add(mod1Sec4Detail);
                    x = await db.SaveChangesAsync() > 0 ? true : false;

                    if (x)
                    {
                        dbContextTransaction.Commit();
                        result = new Notification
                        {
                            id = mod1Sec4Detail.Mod1Sec4DetailId,
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
                            id = mod1Sec4Detail.Mod1Sec4DetailId,
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
                string query = $@"UPDATE [dbo].[Mod1Sec4Detail]
                                SET 
                                [RoofHouseStructureTypeId] = {mod1Sec4Detail.RoofHouseStructureTypeId ?? 0}, 
                                [WallHouseStructureTypeId] = {mod1Sec4Detail.WallHouseStructureTypeId ?? 0}, 
                                [FloorHouseStructureTypeId] = {mod1Sec4Detail.FloorHouseStructureTypeId ?? 0}, 
                                [HasElectricity] = {mod1Sec4Detail.HasElectricity ?? 0}, 
                                [DrinkingWaterSource] = '{mod1Sec4Detail.DrinkingWaterSource}',
                                [OtherDrinkingWaterSource] = N'{mod1Sec4Detail.OtherDrinkingWaterSource}',
                                [PercentOfHomeCookingFuel] = {mod1Sec4Detail.PercentOfHomeCookingFuel ?? 0},
                                [UsingSanitary] = '{mod1Sec4Detail.UsingSanitary}',
                                [AnyoneSickLastYear] = {mod1Sec4Detail.AnyoneSickLastYear ?? 0},
                                [SickList] = '{mod1Sec4Detail.SickList}',
                                [OtherSickList] = N'{mod1Sec4Detail.OtherSickList}',
                                [HasTakenTreatmentForIllness] = {mod1Sec4Detail.HasTakenTreatmentForIllness ?? 0},
                                [TreatmentList] = '{mod1Sec4Detail.TreatmentList}',
                                [OtherTreatmentList] = N'{mod1Sec4Detail.OtherTreatmentList}'
                                WHERE [MasterDataId] = {mod1Sec4Detail.MasterDataId}";

                x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = mod1Sec4Detail.Mod1Sec4DetailId,
                        status = "success",
                        message = "Data has been updated."
                    };
                }
                else
                {
                    string error = $"Data not updated!<br />{msg}";

                    result = new Notification
                    {
                        id = mod1Sec4Detail.Mod1Sec4DetailId,
                        status = "error",
                        message = "",
                        exception = error
                    };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion        

        #region সেকশন ৫ নারীর সক্ষমতা ও ক্ষমতায়ন
        //১.১৭ নিত্য ব্যবহার্য দ্রব্যাদি ও অন্যান্য খাতে ব্যয়ের বিবরন দিন (২০১৯-২০২০)
        [HttpGet]
        public ActionResult GetMod1Sec5Table137(int masterId)
        {
            var list = new List<GetMod1Sec5Table137DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod1Sec5Table137Id, p.MasterDataId, p.WorkForLivingJobType137Id, 
                            s1.OptionName + ' ' + ISNULL(p.Others, '') AS OptionName, p.Others, p.TotalDays, p.AvgHour, p.ApproxEarnLastYear
                            FROM dbo.Mod1Sec5Table137 AS p --p = primary; s = secondary;
                            LEFT JOIN dbo.LookupWorkForLivingJobType137 AS s1 ON p.WorkForLivingJobType137Id = s1.WorkForLivingJobType137Id
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.WorkForLivingJobType137Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod1Sec5Table137DataTemp
                                  {
                                      RowId = dr["Mod1Sec5Table137Id"].ToString().ToInt32(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      WorkForLivingJobType137Id = dr["WorkForLivingJobType137Id"].ToString().ToInt32(),
                                      OptionName = dr["OptionName"].ToString(),
                                      Others = dr["Others"].ToString(),
                                      TotalDays = dr["TotalDays"].ToString().ToInt(),
                                      AvgHour = dr["AvgHour"].ToString().ToInt(),
                                      ApproxEarnLastYear = dr["ApproxEarnLastYear"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod1Sec5Table137DataTemp>();
                }
            }
            catch (Exception)
            {
                list = new List<GetMod1Sec5Table137DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod1Sec5Table137(Mod1Sec5Table137 data)
        {
            try
            {
                db.Mod1Sec5Table137.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod1Sec5Table137(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Module 2


        #region ২.১ খানার তত্ত্বাবধানে কৃষি জমির পরিমাণ
        [HttpGet]
        public ActionResult GetMod2Sec1Table21(int masterId)
        {
            var list = new List<GetMod2Sec1Table21DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec1Table21Id, p.MasterDataId, p.HouseholdAgriLand21Id, 
                            s1.OptionName, p.TotalLand, p.Crop_1, p.Crop_2, p.Crop_3
                            FROM dbo.Mod2Sec1Table21 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdAgriLand21 AS s1 ON p.HouseholdAgriLand21Id = s1.HouseholdAgriLand21Id
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.HouseholdAgriLand21Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec1Table21DataTemp
                                  {
                                      RowId = dr["Mod2Sec1Table21Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      HouseholdAgriLand21Id = dr["HouseholdAgriLand21Id"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      TotalLand = dr["TotalLand"].ToString().ToDecimal(),
                                      Crop_1 = dr["Crop_1"].ToString().ToDecimal(),
                                      Crop_2 = dr["Crop_2"].ToString().ToDecimal(),
                                      Crop_3 = dr["Crop_3"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec1Table21DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod2Sec1Table21DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec1Table21(Mod2Sec1Table21 data)
        {
            try
            {
                db.Mod2Sec1Table21.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec1Table21(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ২.২ খানার চাষাধীন কৃষি জমির প্রকারভেদ (স্বাভাবিক প্লাবন অনুযায়ী)
        [HttpGet]
        public ActionResult GetMod2Sec1Table22(int masterId)
        {
            var list = new List<GetMod2Sec1Table22DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec1Table22Id, p.MasterDataId, p.HouseholdAgriLandType22Id, s1.OptionName, p.AmountCultiAgriLand
                            FROM dbo.Mod2Sec1Table22 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdAgriLandType22 AS s1 ON p.HouseholdAgriLandType22Id = s1.HouseholdAgriLandType22Id
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.HouseholdAgriLandType22Id";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec1Table22DataTemp
                                  {
                                      RowId = dr["Mod2Sec1Table22Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      HouseholdAgriLandType22Id = dr["HouseholdAgriLandType22Id"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      AmountCultiAgriLand = dr["AmountCultiAgriLand"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec1Table22DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod2Sec1Table22DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec1Table22(Mod2Sec1Table22 data)
        {
            try
            {
                db.Mod2Sec1Table22.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec1Table22(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ২.৩	বিভিন্ন ধরনের ফসলের প্রাপ্তি, উৎপাদন ও প্রদত্ত পরিমাণ (গত এক বছরের- ১৪২১ বাংলা) (প্রধান ৫টি ফসল)
        [HttpGet]
        public ActionResult GetMod2Sec1Table23(int masterId)
        {
            var list = new List<GetMod2Sec1Table23DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec1Table23Id, p.MasterDataId, p.HouseholdLandTypeId, s2.OptionName, 
                            p.CropCodeId, s1.OptionName + ' ' + ISNULL(p.OtherCropCode, '') AS CropCodeName, p.OtherCropCode, p.LandAmntIrrigatedLand, p.LandAmntWithoutIrrigation, 
                            p.CropProdAmntIrrigatedLand, p.CropProdAmntWithoutIrrigation, p.TotalValueCropReceived, p.TotalValueByproducts, p.TotalCropYield, p.ShareholderShare
                            FROM dbo.Mod2Sec1Table23 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s1 ON p.CropCodeId = s1.CropCodeId 
                            LEFT JOIN dbo.LookupMod2HouseholdLandType AS s2 ON p.HouseholdLandTypeId = s2.HouseholdLandTypeId
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec1Table23DataTemp
                                  {
                                      RowId = dr["Mod2Sec1Table23Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      HouseholdLandTypeId = dr["HouseholdLandTypeId"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      CropCodeId = dr["CropCodeId"].ToString().ToInt(),
                                      CropCodeName = dr["CropCodeName"].ToString(),
                                      OtherCropCode = dr["OtherCropCode"].ToString(),
                                      LandAmntIrrigatedLand = dr["LandAmntIrrigatedLand"].ToString().ToDecimal(),
                                      LandAmntWithoutIrrigation = dr["LandAmntWithoutIrrigation"].ToString().ToDecimal(),
                                      CropProdAmntIrrigatedLand = dr["CropProdAmntIrrigatedLand"].ToString().ToDecimal(),
                                      CropProdAmntWithoutIrrigation = dr["CropProdAmntWithoutIrrigation"].ToString().ToDecimal(),
                                      TotalValueCropReceived = dr["TotalValueCropReceived"].ToString().ToDecimal(),
                                      TotalValueByproducts = dr["TotalValueByproducts"].ToString().ToDecimal(),
                                      TotalCropYield = dr["TotalCropYield"].ToString().ToDecimal(),
                                      ShareholderShare = dr["ShareholderShare"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec1Table23DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod2Sec1Table23DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec1Table23(Mod2Sec1Table23 data)
        {
            try
            {
                db.Mod2Sec1Table23.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec1Table23(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ২.৪	চাষকৃত জমিতে ফসল উৎপাদনে বিভিন্ন খরচের হিসাব (গত এক বছরের-বাংলা)
        [HttpGet]
        public ActionResult GetMod2Sec1Table24(int masterId)
        {
            var list = new List<GetMod2Sec1Table24DataTemp>();
            try
            {
                var query = $@"SELECT  p.Mod2Sec1Table24Id, p.MasterDataId, p.T24HouseholdLandTypeId, s1.OptionName, 
                            p.T24CropCodeId, s2.OptionName + ' ' + ISNULL(p.OtherT24CropCode, '') AS CropCodeName, p.OtherT24CropCode, 
                            p.UreaAmount, p.UreaValue, p.PotashTspAmount, p.PotashTspValue, 
                            p.PesticidesAmount, p.TotalCostIrrigation, p.SeedsAmount, p.SeedsValue, p.PowerTillerTaka, 
                            p.SelfEmployedLaborDays, p.RentLaborDays, p.DailyLaborCost, p.TotalCost
                            FROM dbo.Mod2Sec1Table24 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s2 ON p.T24CropCodeId = s2.CropCodeId 
                            LEFT JOIN dbo.LookupMod2HouseholdLandType AS s1 ON p.T24HouseholdLandTypeId = s1.HouseholdLandTypeId
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.T24CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec1Table24DataTemp
                                  {
                                      RowId = dr["Mod2Sec1Table24Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      T24HouseholdLandTypeId = dr["T24HouseholdLandTypeId"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      T24CropCodeId = dr["T24CropCodeId"].ToString().ToInt(),
                                      CropCodeName = dr["CropCodeName"].ToString(),
                                      OtherT24CropCode = dr["OtherT24CropCode"].ToString(),
                                      UreaAmount = dr["UreaAmount"].ToString().ToDecimal(),
                                      UreaValue = dr["UreaValue"].ToString().ToDecimal(),
                                      PotashTspAmount = dr["PotashTspAmount"].ToString().ToDecimal(),
                                      PotashTspValue = dr["PotashTspValue"].ToString().ToDecimal(),
                                      PesticidesAmount = dr["PesticidesAmount"].ToString().ToDecimal(),
                                      TotalCostIrrigation = dr["TotalCostIrrigation"].ToString().ToDecimal(),
                                      SeedsAmount = dr["SeedsAmount"].ToString().ToDecimal(),
                                      SeedsValue = dr["SeedsValue"].ToString().ToDecimal(),
                                      PowerTillerTaka = dr["PowerTillerTaka"].ToString().ToDecimal(),
                                      SelfEmployedLaborDays = dr["SelfEmployedLaborDays"].ToString().ToInt(),
                                      RentLaborDays = dr["RentLaborDays"].ToString().ToInt(),
                                      DailyLaborCost = dr["DailyLaborCost"].ToString().ToDecimal(),
                                      TotalCost = dr["TotalCost"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec1Table24DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod2Sec1Table24DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec1Table24(Mod2Sec1Table24 data)
        {
            try
            {
                db.Mod2Sec1Table24.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec1Table24(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ২.৫	বিভিন্ন ফসলে গৃহীত সেচ ব্যবস্থাবলী (যদি প্রযোজ্য হয়)
        [HttpGet]
        public ActionResult GetMod2Sec1Table25(int masterId)
        {
            var list = new List<GetMod2Sec1Table25DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec1Table25Id, p.MasterDataId, 
                            p.T25CropCodeId, s4.OptionName, 
                            p.WaterSourceCodeId, s3.OptionName AS WaterSourceOptionName, 
                            p.IrrigationSysCodeId, s2.OptionName AS IrrigationSysOptionName, 
                            p.AvailabilityCodeId, s1.OptionName AS AvailabilityOptionName
                            FROM dbo.Mod2Sec1Table25 AS p 
                            LEFT JOIN dbo.LookupMod2AvailabilityCode AS s1 ON p.AvailabilityCodeId = s1.AvailabilityCodeId 
                            LEFT JOIN dbo.LookupMod2IrrigationSysCode AS s2 ON p.IrrigationSysCodeId = s2.IrrigationSysCodeId 
                            LEFT JOIN dbo.LookupMod2WaterSourceCode AS s3 ON p.WaterSourceCodeId = s3.WaterSourceCodeId 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s4 ON p.T25CropCodeId = s4.CropCodeId
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.T25CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec1Table25DataTemp
                                  {
                                      RowId = dr["Mod2Sec1Table25Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      T25CropCodeId = dr["T25CropCodeId"].ToString().ToInt(),
                                      OptionName = dr["OptionName"].ToString(),
                                      WaterSourceCodeId = dr["WaterSourceCodeId"].ToString().ToInt(),
                                      WaterSourceOptionName = dr["WaterSourceOptionName"].ToString(),
                                      IrrigationSysCodeId = dr["IrrigationSysCodeId"].ToString().ToInt(),
                                      IrrigationSysOptionName = dr["IrrigationSysOptionName"].ToString(),
                                      AvailabilityCodeId = dr["AvailabilityCodeId"].ToString().ToInt(),
                                      AvailabilityOptionName = dr["AvailabilityOptionName"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec1Table25DataTemp>();
                }
            }
            catch (Exception ex)
            {
                list = new List<GetMod2Sec1Table25DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec1Table25(Mod2Sec1Table25 data)
        {
            try
            {
                db.Mod2Sec1Table25.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec1Table25(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                //var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod1Sec1Table116Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ২.৭	ক)	শষ্য প্রক্রিয়াকরণ, সংরক্ষণ ইত্যাদি ব্যাপারে আপনার কি পর্যাপ্ত ব্যবস্থা আছে?

        [HttpGet]
        public ActionResult GetMod2Sec2Table27(int masterId)
        {
            var list = new List<GetMod2Sec2Table27DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec2Table27Id, p.MasterDataId, 
                            p.CropCodeId, s1.OptionName AS CropCodeOptionName, 
                            p.CropProcessingId, s2.OptionName AS CropProcessingOptionName, 
                            p.CropDryProcessingId, s2.OptionName AS CropDryProcessingOptionName,
                            p.CropStoreProcessingId, s2.OptionName AS CropStoreProcessingOptionName, 
                            p.CropMarketingId, s3.OptionName AS CropMarketingOptionName
                            FROM dbo.Mod2Sec2Table27 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s1 ON p.CropCodeId = s1.CropCodeId
                            LEFT JOIN dbo.LookupMod2CropProcessing AS s2 ON p.CropProcessingId = s2.CropProcessingId 
                            LEFT JOIN dbo.LookupMod2CropMarketing AS s3 ON p.CropMarketingId = s3.CropMarketingId 
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec2Table27DataTemp
                                  {
                                      RowId = dr["Mod2Sec2Table27Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      OptionName = dr["CropCodeOptionName"].ToString(),
                                      CropProcessingOptionName = dr["CropProcessingOptionName"].ToString(),
                                      CropDryProcessingOptionName = dr["CropDryProcessingOptionName"].ToString(),
                                      CropStoreProcessingOptionName = dr["CropStoreProcessingOptionName"].ToString(),
                                      CropMarketingOptionName = dr["CropMarketingOptionName"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec2Table27DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod2Sec2Table27DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec2Table27(Mod2Sec2Table27 data)
        {
            try
            {
                db.Mod2Sec2Table27.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec2Table27(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ২.৮ শস্য বাজারজাতকরণ ও বাজারমূল্য

        [HttpGet]
        public ActionResult GetMod2Sec2Table28(int masterId)
        {
            var list = new List<GetMod2Sec2Table28DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec2Table28Id, p.MasterDataId, p.CropCodeId, s1.OptionName AS CropCodeOptionName, 
                            p.TotalCropProduction, p.TotalCropProdSelfUse, p.RestMarketCropProd, p.FieldSaleCropProdAmount, 
                            p.FieldSaleCropProdValue, p.MarketName, p.DistanceOfLandToMarket, p.TravelCostPerMon, p.TotalCost
                            FROM dbo.Mod2Sec2Table28 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s1 ON p.CropCodeId = s1.CropCodeId
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec2Table28DataTemp
                                  {
                                      RowId = dr["Mod2Sec2Table28Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      OptionName = dr["CropCodeOptionName"].ToString(),
                                      TotalCropProduction = dr["TotalCropProduction"].ToString().ToDecimal(),
                                      TotalCropProdSelfUse = dr["TotalCropProdSelfUse"].ToString().ToDecimal(),
                                      RestMarketCropProd = dr["RestMarketCropProd"].ToString().ToDecimal(),
                                      FieldSaleCropProdAmount = dr["FieldSaleCropProdAmount"].ToString().ToDecimal(),
                                      FieldSaleCropProdValue = dr["FieldSaleCropProdValue"].ToString().ToDecimal(),
                                      MarketName = dr["MarketName"].ToString(),
                                      DistanceOfLandToMarket = dr["DistanceOfLandToMarket"].ToString().ToDecimal(),
                                      TravelCostPerMon = dr["TravelCostPerMon"].ToString().ToDecimal(),
                                      TotalCost = dr["TotalCost"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec2Table28DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod2Sec2Table28DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec2Table28(Mod2Sec2Table28 data)
        {
            try
            {
                db.Mod2Sec2Table28.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec2Table28(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ২.৯ ফসলের ক্ষয়ক্ষতির পরিমাণ

        [HttpGet]
        public ActionResult GetMod2Sec3Table29(int masterId)
        {
            var list = new List<GetMod2Sec3Table29DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod2Sec3Table29Id, p.MasterDataId, p.CropCodeId, s1.OptionName AS CropCodeOptionName, 
                            p.ProdDamageCodeId, p.ProdDamageReasonCodeId, s2.OptionName AS ProdDamageCodeName, s3.OptionName AS ProdDamageReasonCodeName 
                            FROM dbo.Mod2Sec3Table29 AS p 
                            LEFT JOIN dbo.LookupMod2HouseholdCropCode AS s1 ON p.CropCodeId = s1.CropCodeId
                            LEFT JOIN dbo.LookupMod2ProdDamageCode AS s2 ON p.ProdDamageCodeId = s2.ProdDamageCodeId
                            LEFT JOIN dbo.LookupMod2ProdDamageReasonCode AS s3 ON p.ProdDamageReasonCodeId = s3.ProdDamageReasonCodeId
                            WHERE p.MasterDataId = {masterId}
                            ORDER BY p.CropCodeId";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod2Sec3Table29DataTemp
                                  {
                                      RowId = dr["Mod2Sec3Table29Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      OptionName = dr["CropCodeOptionName"].ToString(),
                                      ProdDamageCodeName = dr["ProdDamageCodeName"].ToString(),
                                      ProdDamageReasonCodeName = dr["ProdDamageReasonCodeName"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod2Sec3Table29DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod2Sec3Table29DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod2Sec3Table29(Mod2Sec3Table29 data)
        {
            try
            {
                db.Mod2Sec3Table29.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod2Sec3Table29(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ৩.১	পানি ব্যবস্থাপনা সংক্রান্ত ছকটি পূরণ করুন 

        [HttpGet]
        public ActionResult GetMod3Table31(int masterId)
        {
            var list = new List<GetMod3Table31DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table31Id, p.MasterDataId, p.Mod3TypeOfChanges31Id, p.PresentCondition, p.Mod3ImpactOfSubProject31Id, s1.OptionName AS Mod3TypeOfChanges31Name, 
                            s2.OptionName AS Mod3ImpactOfSubProject31Name
                            FROM dbo.Mod3Table31 AS p 
                            LEFT JOIN dbo.LookupMod3TypeOfChanges31 AS s1 ON p.Mod3TypeOfChanges31Id = s1.Mod3TypeOfChanges31Id
                            LEFT JOIN dbo.LookupMod3ImpactOfSubProject31 AS s2 ON p.Mod3ImpactOfSubProject31Id = s2.Mod3ImpactOfSubProject31Id
                            WHERE p.MasterDataId = {masterId}";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table31DataTemp
                                  {
                                      RowId = dr["Mod3Table31Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      Mod3TypeOfChanges31Name = dr["Mod3TypeOfChanges31Name"].ToString(),
                                      PresentCondition = dr["PresentCondition"].ToString().ToDecimal(),
                                      Mod3ImpactOfSubProject31Name = dr["Mod3ImpactOfSubProject31Name"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod3Table31DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table31DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table31(Mod3Table31 data)
        {
            try
            {
                db.Mod3Table31.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table31(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ৩.২	আপনার এলাকায় সাধারণত পানি সংক্রান্ত কী কী প্রাকৃতিক দুর্যোগ লক্ষ করা যায়?  দুর্যোগের পৌন:পুনিকতা, ব্যাপকতা ও মাত্রা কেমন?

        [HttpGet]
        public ActionResult GetMod3Table32(int masterId)
        {
            var list = new List<GetMod3Table32DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table31Id, p.MasterDataId, s1.OptionName AS Mod3NaturalDisaster32Name, s2.OptionName AS Mod3Recurrence32Name
                            , s3.OptionName AS Mod3Extension32Name, s4.OptionName AS Mod3Dimension32Name
                            FROM dbo.Mod3Table32 AS p 
                            LEFT JOIN dbo.LookupMod3NaturalDisaster32 AS s1 ON p.Mod3NaturalDisaster32Id = s1.Mod3NaturalDisaster32Id
                            LEFT JOIN dbo.LookupMod3Recurrence32 AS s2 ON p.Mod3Recurrence32Id = s2.Mod3Recurrence32Id
                            LEFT JOIN dbo.LookupMod3Extension32 AS s3 ON p.Mod3Extension32Id = s3.Mod3Extension32Id
                            LEFT JOIN dbo.LookupMod3Dimension32 AS s4 ON p.Mod3Dimension32Id = s4.Mod3Dimension32Id
                            WHERE p.MasterDataId = {masterId}";

                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table32DataTemp
                                  {
                                      RowId = dr["Mod3Table31Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      Mod3NaturalDisaster32Name = dr["Mod3NaturalDisaster32Name"].ToString(),
                                      Mod3Recurrence32Name = dr["Mod3Recurrence32Name"].ToString(),
                                      Mod3Extension32Name = dr["Mod3Extension32Name"].ToString(),
                                      Mod3Dimension32Name = dr["Mod3Dimension32Name"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod3Table32DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table32DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table32(Mod3Table32 data)
        {
            try
            {
                db.Mod3Table32.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table32(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE Mod3Table31Id = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region ৩.৩ চাষকৃত জমির প্রকারভেদ (স্বাভাবিক প্লাবন অনুযায়ী)

        [HttpGet]
        public ActionResult GetMod3Table33(int masterId)
        {
            var list = new List<GetMod3Table33DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table33Id, p.MasterDataId, p.Mod3DrySeaWaterMngLandType33Id, p.AmountCultivatedAgriLand, s1.OptionName AS Mod3DrySeaWaterMngLandType33Name
                            FROM dbo.Mod3Table33 AS p 
                            LEFT JOIN dbo.LookupMod3DrySeaWaterMngLandType33 AS s1 ON p.Mod3DrySeaWaterMngLandType33Id = s1.Mod3DrySeaWaterMngLandType33Id
                            WHERE p.MasterDataId = {masterId}";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table33DataTemp
                                  {
                                      RowId = dr["Mod3Table33Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      Mod3DrySeaWaterMngLandType33Name = dr["Mod3DrySeaWaterMngLandType33Name"].ToString(),
                                      AmountCultivatedAgriLand = dr["AmountCultivatedAgriLand"].ToString().ToDecimal()
                                  });
                }
                else
                {
                    list = new List<GetMod3Table33DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table33DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table33(Mod3Table33 data)
        {
            try
            {
                db.Mod3Table33.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table33(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ৩.৫ উত্তর হ্যা হলে

        [HttpGet]
        public ActionResult GetMod3Table35(int masterId)
        {
            var list = new List<GetMod3Table35DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table35Id, p.MasterDataId, s1.OptionName AS Mod3IrrigatedAgriLandType35Name, p.HasIrrigation, s2.OptionName AS DeepTubeWellFuelCode35Name,
                            s3.OptionName AS ShallowTubeWellFuelCode35Name, s4.OptionName AS PowerPumpTubeWellFuelCode35Name, s5.OptionName AS IrrigationDrainFuelCode35Name, s6.OptionName AS IndigenousMethodFuelCode35Name      
                            FROM dbo.Mod3Table35 AS p 
                            LEFT JOIN dbo.LookupMod3IrrigatedAgriLandType35 AS s1 ON p.Mod3IrrigatedAgriLandType35Id = s1.Mod3IrrigatedAgriLandType35Id
                            LEFT JOIN dbo.LookupMod3FuelCode35 AS s2 ON p.DeepTubeWellFuelCode35Id = s2.FuelCode35Id
                            LEFT JOIN dbo.LookupMod3FuelCode35 AS s3 ON p.ShallowTubeWellFuelCode35Id = s3.FuelCode35Id
                            LEFT JOIN dbo.LookupMod3FuelCode35 AS s4 ON p.PowerPumpTubeWellFuelCode35Id = s4.FuelCode35Id
                            LEFT JOIN dbo.LookupMod3FuelCode35 AS s5 ON p.IrrigationDrainFuelCode35Id = s5.FuelCode35Id
                            LEFT JOIN dbo.LookupMod3FuelCode35 AS s6 ON p.IndigenousMethodFuelCode35Id = s6.FuelCode35Id
                            WHERE p.MasterDataId = {masterId}";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table35DataTemp 
                                  {
                                      RowId = dr["Mod3Table35Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      Mod3IrrigatedAgriLandType35Name = dr["Mod3IrrigatedAgriLandType35Name"].ToString(),
                                      HasIrrigation = dr["HasIrrigation"].ToString().ToInt32() == 1 ? "হ্যা" : "না",
                                      DeepTubeWellFuelCode35Name = dr["DeepTubeWellFuelCode35Name"].ToString(),
                                      ShallowTubeWellFuelCode35Name = dr["ShallowTubeWellFuelCode35Name"].ToString(),
                                      PowerPumpTubeWellFuelCode35Name = dr["PowerPumpTubeWellFuelCode35Name"].ToString(),
                                      IrrigationDrainFuelCode35Name = dr["IrrigationDrainFuelCode35Name"].ToString(),
                                      IndigenousMethodFuelCode35Name = dr["IndigenousMethodFuelCode35Name"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod3Table35DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table35DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table35(Mod3Table35 data)
        {
            try
            {
                db.Mod3Table35.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table35(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ৩.৬	 গৃহস্থালী কাজে পানির ব্যবহার

        [HttpGet]
        public ActionResult GetMod3Table36(int masterId)
        {
            var list = new List<GetMod3Table36DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table36Id, p.MasterDataId, s1.OptionName AS HouseholdWaterUse36Name, s2.OptionName AS HouseholdWaterSource36Name,
                            s3.OptionName AS HouseholdOwnershipCode36Name, s4.OptionName AS HouseholdWaterProperties36Name, s5.OptionName AS HouseholdWaterArsenic36Name     
                            FROM dbo.Mod3Table36 AS p 
                            LEFT JOIN dbo.LookupMod3HouseholdWaterUse36 AS s1 ON p.HouseholdWaterUse36Id = s1.HouseholdWaterUse36Id
                            LEFT JOIN dbo.LookupMod3HouseholdWaterSource36 AS s2 ON p.HouseholdWaterSource36Id = s2.HouseholdWaterSource36Id
                            LEFT JOIN dbo.LookupMod3HouseholdOwnershipCode36 AS s3 ON p.HouseholdOwnershipCode36Id = s3.HouseholdOwnershipCode36Id
                            LEFT JOIN dbo.LookupMod3HouseholdWaterProperties36 AS s4 ON p.HouseholdWaterProperties36Id = s4.HouseholdWaterProperties36Id
                            LEFT JOIN dbo.LookupMod3HouseholdWaterArsenic36 AS s5 ON p.HouseholdWaterArsenic36Id = s5.HouseholdWaterArsenic36Id
                            WHERE p.MasterDataId = {masterId}";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table36DataTemp
                                  {
                                      RowId = dr["Mod3Table36Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      HouseholdWaterUse36Name = dr["HouseholdWaterUse36Name"].ToString(),
                                      HouseholdWaterSource36Name = dr["HouseholdWaterSource36Name"].ToString(),
                                      HouseholdOwnershipCode36Name = dr["HouseholdOwnershipCode36Name"].ToString(),
                                      HouseholdWaterProperties36Name = dr["HouseholdWaterProperties36Name"].ToString(),
                                      HouseholdWaterArsenic36Name = dr["HouseholdWaterArsenic36Name"].ToString()
                                  });
                }
                else
                {
                    list = new List<GetMod3Table36DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table36DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table36(Mod3Table36 data)
        {
            try
            {
                db.Mod3Table36.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table36(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ৩.৮	 উপ-প্রকল্পের বর্তমান অবস্থা কেমন?

        [HttpGet]
        public ActionResult GetMod3Table38(int masterId)
        {
            var list = new List<GetMod3Table38DataTemp>();
            try
            {
                var query = $@"SELECT p.Mod3Table38Id, p.MasterDataId, s1.OptionName AS CurrentStatusSubProj38Name, s2.OptionName AS CurrentStatus38Name, p.NeedRepairDigCanal
                            FROM dbo.Mod3Table38 AS p 
                            LEFT JOIN dbo.LookupMod3CurrentStatusSubProj38 AS s1 ON p.CurrentStatusSubProj38Id = s1.CurrentStatusSubProj38Id
                            LEFT JOIN dbo.LookupMod3CurrentStatus38 AS s2 ON p.CurrentStatus38Id = s2.CurrentStatus38Id
                            WHERE p.MasterDataId = {masterId}";
                var dt = exec.SelectQuery(query, ref msg);

                if (dt.Rows.Count > 0)
                {
                    list.AddRange(from DataRow dr in dt.Rows
                                  select new GetMod3Table38DataTemp
                                  {
                                      RowId = dr["Mod3Table38Id"].ToString().ToInt(),
                                      MasterDataId = dr["MasterDataId"].ToString().ToInt32(),
                                      CurrentStatusSubProj38Name = dr["CurrentStatusSubProj38Name"].ToString(),
                                      CurrentStatus38Name = dr["CurrentStatus38Name"].ToString(),
                                      NeedRepairDigCanalName = dr["NeedRepairDigCanal"].ToString().ToInt() == 1 ? "হ্যা" : "না"
                                  });
                }
                else
                {
                    list = new List<GetMod3Table38DataTemp>();
                }
            }
            catch (Exception exception)
            {
                list = new List<GetMod3Table38DataTemp>();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMod3Table38(Mod3Table38 data)
        {
            try
            {
                db.Mod3Table38.Add(data);
                var response = db.SaveChanges() > 0;

                if (response)
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "success",
                        message = "Data has been saved successfully!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = data.MasterDataId,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = data.MasterDataId,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMod3Table38(string rowId, string tableName)
        {
            try
            {
                var query = $@"DELETE FROM [dbo].[{tableName}] WHERE [{tableName}Id] = {rowId}";
                var x = exec.NonSelectQuery(query, ref msg);

                if (x)
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "success",
                        message = "Data has been deleted!",
                        exception = ""
                    };
                }
                else
                {
                    result = new Notification
                    {
                        id = 0,
                        status = "error",
                        message = msg,
                        exception = msg
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Notification
                {
                    id = 0,
                    status = "error",
                    message = "An error occurred!",
                    exception = ExtractInnerException(ex)
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        // POST: BaseLine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MasterDataId,CaseNo,FormDate,PlaceOfInterview,LocLatDeg,LocLatMin,LocLatSec,LocLatDecimal,LocLongDeg,LocLongMin,LocLongSec,LocLongDecimal,TypeOfRespondent,DivisionCode,DistrictCode,UpazilaCode,UnionCode,Village,Para,WordNo,HouseNo,NameOfInterviewer,InterviewerSignature,NameOfSupervisor,SupervisorSignature,DataDate,IsActive,IsDelete,CreatedBy,CreatedDate,UpdateBy,UpdatedDate,DeletedBy,DeletedDate")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Master.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master);
        }

        // GET: BaseLine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: BaseLine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MasterDataId,CaseNo,FormDate,PlaceOfInterview,LocLatDeg,LocLatMin,LocLatSec,LocLatDecimal,LocLongDeg,LocLongMin,LocLongSec,LocLongDecimal,TypeOfRespondent,DivisionCode,DistrictCode,UpazilaCode,UnionCode,Village,Para,WordNo,HouseNo,NameOfInterviewer,InterviewerSignature,NameOfSupervisor,SupervisorSignature,DataDate,IsActive,IsDelete,CreatedBy,CreatedDate,UpdateBy,UpdatedDate,DeletedBy,DeletedDate")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master);
        }

        // GET: BaseLine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: BaseLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Master.Find(id);
            db.Master.Remove(master);
            db.SaveChanges();
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
