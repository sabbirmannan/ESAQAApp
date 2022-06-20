using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNet.Identity;
using BAC007.Helpers;
using BAC007.Models;
using BAC007.Controllers;
using System.IO;

namespace BAC007
{
    public partial class WebForm : System.Web.UI.Page
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private CommonHelper _ch = new CommonHelper();
        private readonly CommonController _cc = new CommonController();
        private readonly ExecuteClass exec = new ExecuteClass();
        private string msg = string.Empty;
        private Notification result = new Notification();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string lookupClass = "", dataClass = "";
            string query = @"SELECT [TableInfoId],[FormOrder],[WhichPart],[FormTitle],[TableTitle],[LookupTableName],
                             [DataSaveTableName],[IsIndividualTable],[IsResponseCols],[IsFiscalYearFlat],[IsFiscalYearTwoCols] 
                             FROM [dbo].[TableInfoes]
                             WHERE [TableInfoId] in (3,5,6,8,9,10,40,42,43,44,45,46,62)";

            DataTable dt = exec.SelectQuery(query, ref msg);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string lookupTableName = dr["LookupTableName"].ToString();
                    string dataTableName = dr["DataSaveTableName"].ToString();

                    lookupClass = @"using System.ComponentModel.DataAnnotations;
                                    namespace BAC007.Models
                                    {
                                        public class " + lookupTableName + @"
                                        {
                                            [Key]
                                            public int DataId { get; set; }

                                            [Required]
                                            [StringLength(250)]
                                            [Display(Name = ""ItemName"")]
                                            public string ItemName { get; set; }
                                        }
                                    }";

                    dataClass = @"using System.ComponentModel.DataAnnotations;
                                  namespace BAC007.Models
                                  {
                                      public class " + dataTableName + @"
                                      {
                                          [Key]
                                          public int " + dataTableName + @"Id { get; set; }

                                          public int MasterDataId { get; set; }
                                          public int LookupDataId { get; set; }
                                                                                    
                                          [Display(Name = ""2017-18 (#)"")]
                                          public decimal? FyValue_1_Qty { get; set; }

                                          [Display(Name = ""2017-18 (Value)"")]
                                          public decimal? FyValue_1_Value { get; set; }

                                          [Display(Name = ""2018-19 (#)"")]
                                          public decimal? FyValue_2_Qty { get; set; }

                                          [Display(Name = ""2018-19 (Value)"")]
                                          public decimal? FyValue_2_Value { get; set; }

                                          [Display(Name = ""2019-20 (#)"")]
                                          public decimal? FyValue_3_Qty { get; set; }

                                          [Display(Name = ""2019-20 (Value)"")]
                                          public decimal? FyValue_3_Value { get; set; }

                                          [Display(Name = ""2020-21 (#)"")]
                                          public decimal? FyValue_4_Qty { get; set; }

                                          [Display(Name = ""2020-21 (Value)"")]
                                          public decimal? FyValue_4_Value { get; set; }                                          
                                      }
                                  }
                                  ";

                    //File.WriteAllText(@"C:\temp\Lookup\" + lookupTableName + ".txt", lookupClass);
                    File.WriteAllText(@"C:\temp\Data\" + dataTableName + ".txt", dataClass);
                }
            }
        }
    }
}