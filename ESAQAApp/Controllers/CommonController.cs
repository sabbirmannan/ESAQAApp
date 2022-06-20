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
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.AspNet.Identity;
using BAC007.Models;
//using BAC007.ViewModels;
using DAL;
using Newtonsoft.Json;
using BAC007.Helpers;

namespace BAC007.Controllers
{
    public class CommonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ExecuteClass exec = new ExecuteClass();
        string msg = string.Empty;
        public string[] Months = { "0", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        //// GET: WaterQualityCategories
        //[HttpGet]
        //public ActionResult GetWaterQualityCategories()
        //{
        //    var li = (from dist in db.WaterQualityCategories.Where(w => w.IsActive == true && w.IsDelete == false)
        //              select new ListItems
        //              {
        //                  Value = dist.WaterQualityCategoryID,
        //                  Text = dist.CategoryName
        //              }).OrderBy(o => o.Text).ToList();

        //    return Json(li, JsonRequestBehavior.AllowGet);
        //}

        // GET: GetDistrict
        [HttpGet]
        public ActionResult GetDistrict(string DivisionCode)
        {
            var li = (from dist in db.District.Where(w => w.DivisionCode == DivisionCode)
                      select new AdminListItems
                      {
                          Value = dist.DistrictCode,
                          Text = dist.DistrictName
                      }).OrderBy(o => o.Text).ToList();

            return Json(li, JsonRequestBehavior.AllowGet);
        }

        // GET: GetUpazilla
        [HttpGet]
        public ActionResult GetUpazilla(string DistrictCode)
        {
            var li = (from upz in db.Upazila.Where(w => w.DistrictCode == DistrictCode)
                      select new AdminListItems
                      {
                          Value = upz.UpazilaCode,
                          Text = upz.UpazilaName
                      }).OrderBy(o => o.Text).ToList();

            return Json(li, JsonRequestBehavior.AllowGet);
        }

        // GET: GetUnion
        [HttpGet]
        public ActionResult GetUnion(string UpazilaCode)
        {
            var li = (from uni in db.Union.Where(w => w.UpazilaCode == UpazilaCode)
                      select new AdminListItems
                      {
                          Value = uni.UnionCode,
                          Text = uni.UnionName
                      }).OrderBy(o => o.Text).ToList();

            return Json(li, JsonRequestBehavior.AllowGet);
        }


        #region Shape DB to GeoJSON conversion process
        [HttpGet]
        public ActionResult GetMainRiverSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [ID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[AREA],[RIVER_TYPE],[TYPE],[Shape_Leng] FROM [dbo].[mapMainRiverWGS84]", ref msg);

            List<MainRiverGeoLocation> _lstDetailRiver = new List<MainRiverGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDetailRiver.Add(new MainRiverGeoLocation()
                {
                    type = "Feature",
                    properties = new MainRiverObjectProperties()
                    {
                        ID = row["ID"].ToString(),
                        Area = row["AREA"].ToString(),
                        RiverType = row["RIVER_TYPE"].ToString(),
                        Type = row["TYPE"].ToString(),
                        Length = row["Shape_Leng"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            MainRiverFeatures _mrf = new MainRiverFeatures()
            {
                type = "FeatureCollection",
                features = _lstDetailRiver
            };

            string aaaa = _mrf.ToString();

            var result = new ContentResult
            {
                Content = serializer.Serialize(_mrf),
                ContentType = "application/json"
            };

            //string gj = result.Content.ToString();
            //using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/geodata.txt"), false))
            //{
            //    newTask.Write(gj);
            //}

            return result;
            //return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMazorRiverSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[RIVNAME],[RIVER_WIDTH],[LENGTH],[MODELR],[MODELRNM],[MAJORRNM],[MAJORR],[BIWTA_NAME],[BWDB_NAME],[SOB_NAME],[BORDERR],[BRDRCOD],[OFFTAKE],[OUTFALL],[HDREGION],[WIDTH],[REMARKS] FROM [dbo].[mapMajorRiverWGS84]", ref msg);

            List<MazorRiverGeoLocation> _lstMazorRiver = new List<MazorRiverGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstMazorRiver.Add(new MazorRiverGeoLocation()
                {
                    type = "Feature",
                    properties = new MazorRiverObjectProperties()
                    {
                        RiverID = row["OBJECTID"].ToString(),
                        RiverName = row["RIVNAME"].ToString(),
                        Width = row["RIVER_WIDTH"].ToString(),
                        Length = row["LENGTH"].ToString(),
                        ModelerName = row["MODELRNM"].ToString(),
                        MajorName = row["MAJORRNM"].ToString(),
                        BIWTA = row["BIWTA_NAME"].ToString(),
                        BWDB = row["BWDB_NAME"].ToString(),
                        SOB = row["SOB_NAME"].ToString(),
                        Offtake = row["OFFTAKE"].ToString(),
                        Outfall = row["OUTFALL"].ToString(),
                        HdRegion = row["HDREGION"].ToString(),
                        Remarks = row["REMARKS"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            MazorRiverFeatures _mrf = new MazorRiverFeatures()
            {
                type = "FeatureCollection",
                features = _lstMazorRiver
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(_mrf),
                ContentType = "application/json"
            };

            string gj = result.Content.ToString();
            using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/geodata.txt"), false))
            {
                newTask.Write(gj);
            }

            //return result;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetailRiverSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[MAJORRNM],[RIVNAME],[RIV_WIDTH],[OFFTAKE],[OUTFALL],[WIDTH] FROM [dbo].[mapDetailRiverWGS84]", ref msg);

            List<DetailRiverGeoLocation> _lstDetailRiver = new List<DetailRiverGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDetailRiver.Add(new DetailRiverGeoLocation()
                {
                    type = "Feature",
                    properties = new DetailRiverObjectProperties()
                    {
                        majorrivername = row["MAJORRNM"].ToString(),
                        rivername = row["RIVNAME"].ToString(),
                        riverwidth = row["RIV_WIDTH"].ToString(),
                        offtake = row["OFFTAKE"].ToString(),
                        outfall = row["OUTFALL"].ToString(),
                        width = row["WIDTH"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            DetailRiverFeatures _mrf = new DetailRiverFeatures()
            {
                type = "FeatureCollection",
                features = _lstDetailRiver
            };

            string aaaa = _mrf.ToString();

            var result = new ContentResult
            {
                Content = serializer.Serialize(_mrf),
                ContentType = "application/json"
            };

            string gj = result.Content.ToString();
            //using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/DetailRiverWGS84.geojson"), false))
            //{
            //    newTask.Write(gj);
            //}

            //return result;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDivisionSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[DIVCODE],[DIVNAME],[LANDTYPE],[AREA] FROM [dbo].[mapDivisionWGS84]", ref msg);

            List<DivisionGeoLocation> _lstDvision = new List<DivisionGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDvision.Add(new DivisionGeoLocation()
                {
                    type = "Feature",
                    properties = new DivisionObjectProperties()
                    {
                        divcode = row["DIVCODE"].ToString(),
                        divname = row["DIVNAME"].ToString(),
                        landtype = row["LANDTYPE"].ToString(),
                        area = row["AREA"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            DivisionFeatures _df = new DivisionFeatures()
            {
                type = "FeatureCollection",
                features = _lstDvision
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(_df),
                ContentType = "application/json"
            };

            string gj = result.Content.ToString();
            using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/divisionWGS84.geojson"), false))
            {
                newTask.Write(gj);
            }

            //return result;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDistrictSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[DISTCODE],[DISTNAME],[DIVNAME],[LANDTYPE],[AREA] FROM [dbo].[mapDistrictWGS84]", ref msg);

            List<DistrictGeoLocation> _lstDistrict = new List<DistrictGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDistrict.Add(new DistrictGeoLocation()
                {
                    type = "Feature",
                    properties = new DistrictObjectProperties()
                    {
                        distcode = row["DISTCODE"].ToString(),
                        distname = row["DISTNAME"].ToString(),
                        divname = row["DIVNAME"].ToString(),
                        landtype = row["LANDTYPE"].ToString(),
                        area = row["AREA"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            DistrictFeatures _df = new DistrictFeatures()
            {
                type = "FeatureCollection",
                features = _lstDistrict
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(_df),
                ContentType = "application/json"
            };

            string gj = result.Content.ToString();
            using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/districtWGS84.geojson"), false))
            {
                newTask.Write(gj);
            }

            //return result;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStrategicLocationSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            string qry = @"SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[Geocode],[Latitude],[Longitude],
                            [DivName],[DistName],[ThanaName],[UnionName],[MouzaName],[RiverID],[RiverName],[DateOfESTD],
                            [JLNo],[SLID],[SLName],[UseOfWater],[PolutionSource],[IsExist],[Remarks] 
                            FROM [dbo].[mapStrategicLocation]
                            ORDER BY [SLID]";

            DataTable dt = exec.SelectQuery(qry, ref msg);

            List<StrategicLocationGeoLocation> _lstDistrict = new List<StrategicLocationGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDistrict.Add(new StrategicLocationGeoLocation()
                {
                    type = "Feature",
                    properties = new StrategicLocationObjectProperties()
                    {
                        SLID = row["SLID"].ToString(),
                        SLName = row["SLName"].ToString(),
                        RiverID = row["RiverID"].ToString(),
                        RiverName = row["RiverName"].ToString(),
                        Geocode = row["Geocode"].ToString(),
                        Latitude = row["Latitude"].ToString(),
                        Longitude = row["Longitude"].ToString(),
                        Location = row["MouzaName"].ToString() + ", " + row["UnionName"].ToString() + ", " + row["ThanaName"].ToString() + ", " + row["DistName"].ToString() + ", " + row["DivName"].ToString(),
                        DateOfESTD = row["DateOfESTD"].ToString(),
                        UseOfWater = row["UseOfWater"].ToString(),
                        PolutionSource = row["PolutionSource"].ToString(),
                        Status = row["IsExist"].ToString(),
                        Remarks = row["Remarks"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            StrategicLocationFeatures _df = new StrategicLocationFeatures()
            {
                type = "FeatureCollection",
                features = _lstDistrict
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(_df),
                ContentType = "application/json"
            };

            //string gj = result.Content.ToString();
            //using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/StrategicLocation13WGS84.geojson"), false))
            //{
            //    newTask.Write(gj);
            //}

            return result;
            //return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetHotSpotSpatialData()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            DataTable dt = exec.SelectQuery("SELECT [OBJECTID],dbo.geometry2json([Shape]) AS 'GEOMETRY',[Name],[Lat],[Long] FROM [dbo].[MS_OLDWGS84]", ref msg);

            List<HotSpotGeoLocation> _lstDistrict = new List<HotSpotGeoLocation>();

            foreach (DataRow row in dt.Rows)
            {
                _lstDistrict.Add(new HotSpotGeoLocation()
                {
                    type = "Feature",
                    properties = new HotSpotObjectProperties()
                    {
                        ID = row["OBJECTID"].ToString(),
                        Name = row["Name"].ToString(),
                        Lat = row["Lat"].ToString(),
                        Long = row["Long"].ToString()
                    },
                    geometry = row["GEOMETRY"].ToString()
                });
            }

            HotSpotFeatures _df = new HotSpotFeatures()
            {
                type = "FeatureCollection",
                features = _lstDistrict
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(_df),
                ContentType = "application/json"
            };

            //string gj = result.Content.ToString();
            //using (StreamWriter newTask = new StreamWriter(Server.MapPath("~/MapData/Shapes/StrategicLocation13WGS84.geojson"), false))
            //{
            //    newTask.Write(gj);
            //}

            return result;
            //return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public string ConvertToDateTime(string _date, string _time)
        {
            string _datetime = string.Empty;
            string datePart = !string.IsNullOrEmpty(_date) ? DateTime.Parse(_date).ToString("yyyy-MM-dd") : "";
            string timePart = !string.IsNullOrEmpty(_time) ? DateTime.Parse(_time).ToString("HH:mm:ss") : "";

            if (!string.IsNullOrEmpty(datePart) && !string.IsNullOrEmpty(timePart))
            {
                _datetime = DateTime.Parse(datePart + " " + timePart).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (!string.IsNullOrEmpty(datePart) && string.IsNullOrEmpty(timePart))
            {
                _datetime = DateTime.Parse(datePart).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                _datetime = null;
            }

            return _datetime;
        }

        public List<GetYear> GetYearList()
        {
            List<GetYear> _getYear = new List<GetYear>();

            for (var j = DateTime.Now.Year; j >= 1990; j--)
            {
                _getYear.Add(new GetYear() { ID = j, Name = j.ToString() });
            }

            return _getYear;
        }

        public List<GetYear> GetYearList(bool isSmallToBig)
        {
            List<GetYear> _getYear = new List<GetYear>();

            if (isSmallToBig)
            {
                for (var j = 1990; j <= DateTime.Now.Year; j++)
                {
                    _getYear.Add(new GetYear() { ID = j, Name = j.ToString() });
                }
            }
            else
            {
                for (var j = DateTime.Now.Year; j >= 1990; j--)
                {
                    _getYear.Add(new GetYear() { ID = j, Name = j.ToString() });
                }
            }

            return _getYear;
        }

        public int GetPurposeOfSampleName(string posn)
        {
            switch (posn)
            {
                case "Compliance":
                    return 1;

                case "ECC":
                    return 2;

                case "On Payment":
                    return 3;

                case "Routine Monitoring":
                    return 4;

                case "Special Case":
                    return 5;

                default:
                    return 0;
            }
        }

        public int GetTypeOfSample(string tos)
        {
            switch (tos)
            {
                case "Drinking Water":
                    return 1;

                case "Industrial use of Water":
                    return 2;

                case "Agriculture use of Water":
                    return 3;

                case "Fisheries use of Water":
                    return 4;

                case "Ground Water":
                    return 5;

                case "Industrial Effluent after treatment":
                    return 6;

                case "Industrial Effluent at down stream":
                    return 7;

                case "Industrial Effluent at up stream":
                    return 8;

                case "Industrial Effluent before treatment":
                    return 9;

                case "Industrial Effluent mixed with surface water":
                    return 10;

                case "Surface Water":
                    return 11;

                default:
                    return 0;
            }
        }

        public string GetWeatherCondition(string wc)
        {
            switch (wc)
            {
                case "Sunny":
                    return "Sunny";

                case "Cloudy/Foggy":
                    return "CloudyFoggy";

                case "Half Rainy":
                    return "HalfRainy";

                case "Half-Sunny":
                    return "HalfSunny";

                case "Rainy":
                    return "Rainy";

                default:
                    return "";
            }
        }
    }

    public class ImageItem
    {
        public byte[] photo { get; set; }
    }

    public class StringImageItem
    {
        public byte[] photo { get; set; }
    }

    public class ListItems
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class AdminListItems
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class StrategicLocationInfo
    {
        public string River { get; set; }
        public bool? IsMonitoring { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazilla { get; set; }
        public string Union { get; set; }
        public string Village { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class SampleOutcome
    {
        public int SampleResultID { get; set; }
        public int DataCollectionMasterID { get; set; }
        public string ParameterName { get; set; }
        public string Result { get; set; }
        public string Remarks { get; set; }
        private string CM { get; set; }
        public string CollectionMonth
        {
            get
            {
                DateTime tmp;
                DateTime.TryParse(CM.ToString(), out tmp);
                return tmp.ToString("MMM yy");
            }
            set { CM = value; }
        }
    }

    public class ImportedParamResult
    {
        public int Serial { get; set; }
        public string StrategicLocations { get; set; }
        public string River { get; set; }
        public string Parameters { get; set; }
        public string PurposeOfSample { get; set; }
        public string TypeOfSample { get; set; }
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string Mar { get; set; }
        public string Apr { get; set; }
        public string May { get; set; }
        public string Jun { get; set; }
        public string Jul { get; set; }
        public string Aug { get; set; }
        public string Sep { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }
        public string ResultDate { get; set; }
    }

    public class FlotSeries
    {
        public string label;
        public string name;
        public List<double[]> data;
    }

    public class FlotSingleSeries
    {
        public int index;
        public double value;
    }

    public class FlotPieSeries
    {
        public string label;
        public double data;
    }

    public class UnitEqs
    {
        public string parameter;
        public string unit;
        public string eqs;
    }

    #region Main_River
    public class MainRiverFeatures
    {
        public string type { get; set; }
        public List<MainRiverGeoLocation> features { get; set; }
    }

    public class MainRiverGeoLocation
    {
        public string type { get; set; }
        public MainRiverObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class MainRiverObjectProperties
    {
        public string ID { get; set; }
        public string Area { get; set; }
        public string RiverType { get; set; }
        public string Type { get; set; }
        public string Length { get; set; }
    }
    #endregion

    #region Mazor_River
    public class MazorRiverFeatures
    {
        public string type { get; set; }
        public List<MazorRiverGeoLocation> features { get; set; }
    }

    public class MazorRiverGeoLocation
    {
        public string type { get; set; }
        public MazorRiverObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class MazorRiverObjectProperties
    {
        public string RiverID { get; set; }
        public string RiverName { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string ModelerName { get; set; }
        public string MajorName { get; set; }
        public string BIWTA { get; set; }
        public string BWDB { get; set; }
        public string SOB { get; set; }
        public string Offtake { get; set; }
        public string Outfall { get; set; }
        public string HdRegion { get; set; }
        public string Remarks { get; set; }
    }
    #endregion

    #region Detail_River
    public class DetailRiverFeatures
    {
        public string type { get; set; }
        public List<DetailRiverGeoLocation> features { get; set; }
    }

    public class DetailRiverGeoLocation
    {
        public string type { get; set; }
        public DetailRiverObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class DetailRiverObjectProperties
    {
        public string majorrivername { get; set; }
        public string rivername { get; set; }
        public string riverwidth { get; set; }
        public string offtake { get; set; }
        public string outfall { get; set; }
        public string width { get; set; }
    }
    #endregion

    #region Division
    public class DivisionFeatures
    {
        public string type { get; set; }
        public List<DivisionGeoLocation> features { get; set; }
    }

    public class DivisionGeoLocation
    {
        public string type { get; set; }
        public DivisionObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class DivisionObjectProperties
    {
        public string divcode { get; set; }
        public string divname { get; set; }
        public string landtype { get; set; }
        public string area { get; set; }
    }
    #endregion

    #region District
    public class DistrictFeatures
    {
        public string type { get; set; }
        public List<DistrictGeoLocation> features { get; set; }
    }

    public class DistrictGeoLocation
    {
        public string type { get; set; }
        public DistrictObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class DistrictObjectProperties
    {
        public string distcode { get; set; }
        public string distname { get; set; }
        public string divname { get; set; }
        public string landtype { get; set; }
        public string area { get; set; }
        //public string tpop_01 { get; set; }
        //public string male { get; set; }
        //public string female { get; set; }
    }
    #endregion

    #region Strategic_Location
    public class StrategicLocationFeatures
    {
        public string type { get; set; }
        public List<StrategicLocationGeoLocation> features { get; set; }
    }

    public class StrategicLocationGeoLocation
    {
        public string type { get; set; }
        public StrategicLocationObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class StrategicLocationObjectProperties
    {
        public string SLID { get; set; }
        public string SLName { get; set; }
        public string RiverID { get; set; }
        public string RiverName { get; set; }
        public string Geocode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public string DateOfESTD { get; set; }
        public string UseOfWater { get; set; }
        public string PolutionSource { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
    #endregion

    #region Hot_Spot_Location
    public class HotSpotFeatures
    {
        public string type { get; set; }
        public List<HotSpotGeoLocation> features { get; set; }
    }

    public class HotSpotGeoLocation
    {
        public string type { get; set; }
        public HotSpotObjectProperties properties { get; set; }
        public string geometry { get; set; }
    }

    public class HotSpotObjectProperties
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
    #endregion

    #region Inline Water Quality Parameter
    public class InlineWaterQualityParameter
    {
        public int DataCollectionMasterID { get; set; }
        public List<ParameterResult> ParameterResults { get; set; }
        public string Remarks { get; set; }
        public string CollectionMonth { get; set; }
    }

    public class ParameterResult
    {
        public string ParameterName { get; set; }
        public string Result { get; set; }
    }

    public class InlineWQPResult
    {
        public int DCMID { get; set; }
        public bool RESULT { get; set; }
        public string EXCEPTION { get; set; }
    }

    public class Notification
    {
        public int id { get; set; }
        public string caseno { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string exception { get; set; }
    }
    #endregion

    #region Monitoring Report
    public class MonitoringReport
    {
        public string River { get; set; }
        public string Location { get; set; }
        public string LabCode { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TempField { get; set; }
        public string TempLab { get; set; }
        public string PhField { get; set; }
        public string PhLab { get; set; }
        public string ECmicro { get; set; }
        public string Chloride { get; set; }
        public string ResidualChlorine { get; set; }
        public string P_Alkalinity { get; set; }
        public string T_Alkalinity { get; set; }
        public string TurbidityITU { get; set; }
        public string SettleableSolid { get; set; }
        public string TS { get; set; }
        public string TDS { get; set; }
        public string SS { get; set; }
        public string TKN { get; set; }
        public string Ammonia { get; set; }
        public string TVBC { get; set; }
        public string TotalColiform { get; set; }
        public string FecalColiform { get; set; }
        public string DO { get; set; }
        public string BOD { get; set; }
        public string COD { get; set; }
        public string CalciumHardness { get; set; }
        public string TotalHardness { get; set; }
        public string Nitrate { get; set; }
        public string Nitrite { get; set; }
        public string NH4N { get; set; }
        public string Sulphate { get; set; }
        public string Phosphate { get; set; }
        public string Lead { get; set; }
        public string Chromium { get; set; }
        public string Cadmium { get; set; }
        public string Arsenic { get; set; }
        public string Iron { get; set; }
        public string OtherMetal { get; set; }
        public string OtherNonMetal { get; set; }
        public string Remarks { get; set; }
    }

    public class StrategicLocationReport
    {
        public string RiverID { get; set; }
        public string RiverName { get; set; }
        public string StrategicLocationID { get; set; }
        public string StrategicLocationName { get; set; }
        public string Criteria { get; set; }
    }
    #endregion

    #region
    public class GetParameterResult
    {
        public int SampleResultID { get; set; }
        public string ParameterName { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string CollectionMonth { get; set; }
        public string Remarks { get; set; }
        public string CustomID { get; set; }
        public string StrategicLocation { get; set; }
    }

    public class GetYear
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    #endregion
}