using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Globalization;

namespace ESAQAApp.Helpers
{
    public static class Utility
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            List<string> SheetNameList = new List<string>();
            try
            {
                oledbConn.Open();

                DataTable dbSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dbSchema == null || dbSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the worksheet.");
                }

                foreach (DataRow dr in dbSchema.Rows)
                {
                    SheetNameList.Add(dr["TABLE_NAME"].ToString());
                }

                string dataSheetName = SheetNameList.Where(w => w.ToLower().Contains("datasheet")).FirstOrDefault();

                if (!string.IsNullOrEmpty(dataSheetName))
                {
                    //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + dataSheetName + "]", oledbConn);
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
                else
                {
                    throw new Exception("Error: Please rename your excel data sheet as DataSheet.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oledbConn.Close();
            }

            return dt;
        }

        public delegate bool TryParseDelegate<T>(string s, out T t);

        public static T? TryParseNullable<T>(string s, TryParseDelegate<T> tryParse) where T : struct
        {
            if (string.IsNullOrEmpty(s))
                return null;
            T t;

            if (tryParse(s, out t))
                return t;

            return null;
        }

        /// <summary>
        /// Convert UI date format to database date format. e.g. dd/MM/yyyy -> yyyy-MM-dd
        /// </summary>
        /// <param name="_date">Input date format should be: 25/12/1991 [dd/MM/yyyy]</param>
        /// <returns>string</returns>
        public static string ToDatabaseDateFormat(this string _date)
        {
            string result = string.Empty;
            string[] _dateArray = _date.Split('/');

            if (_dateArray.Length > 0)
            {
                result = _dateArray[2].ToString() + "-" + _dateArray[1].ToString() + "-" + _dateArray[0].ToString();
            }

            return result;
        }

        /// <summary>
        /// Convert UI date time format to database date time format. e.g. i.p = dd/MM/yyyy HH:mm:ss -> o.p = yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="_datetime">Input date time format should be: 25/12/1991 21:30:45 [dd/MM/yyyy HH:mm:ss]</param>
        /// <returns>string</returns>
        public static string ToDatabaseDateTimeFormat(this string _datetime)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(_datetime))
            {
                string[] _dtArray = _datetime.Split(' ');
                string[] _datepart = _dtArray[0].Split('/');

                if (_datepart.Length > 0)
                {
                    result = _datepart[2].ToString() + "-" + _datepart[1].ToString() + "-" + _datepart[0].ToString();
                    result = result + " " + _dtArray[1];
                }
            }

            return result;
        }

        /// <summary>
        /// Convert UI date format to database date format. e.g. yyyy-MM-dd -> dd/MM/yyyy
        /// </summary>
        /// <param name="_date">Input date format should be: 2020-10-09 -> Output: 25/12/1991 [dd/MM/yyyy]</param>
        /// <returns>string</returns>
        public static string DatabaseDateToUiDateFormat(this string _date)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(_date) && !_date.Equals("NULL"))
            {
                string[] _dateArray = _date.Split('-');

                if (_dateArray.Length > 0)
                {
                    result = _dateArray[2].ToString() + "/" + _dateArray[1].ToString() + "/" + _dateArray[0].ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// Convert database date format to UI date format. e.g. yyyy-MM-dd -> dd/MM/yyyy
        /// </summary>
        /// <param name="_date">Input date format should be: 1991-12-25 [yyyy-MM-dd]</param>
        /// <returns>string</returns>
        public static string ToUIDateFormat(this string _date)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(_date) && !_date.Equals("NULL"))
            {
                result = DateTime.Parse(_date).ToString("dd/MM/yyyy");
            }

            return result;
        }

        /// <summary>
        /// Convert database date format to UI date format as 09 Oct, 2019. e.g. yyyy-MM-dd -> dd MMM, yyyy
        /// </summary>
        /// <param name="_date">Input date format should be: 2019-10-09 or 09/10/2019 [yyyy-MM-dd or dd/MM/yyyy]</param>
        /// <returns>string</returns>
        public static string ToddMMMyyyyDateFormat(this string _date)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(_date) && !_date.Equals("NULL"))
            {
                result = DateTime.Parse(_date).ToString("dd MMM, yyyy");
            }

            return result;
        }

        public static string ToCommaMoney(this string val)
        {
            string money = string.Empty;

            if (!string.IsNullOrEmpty(val))
            {
                money = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(val));
            }
            else
            {
                money = "0.00";
            }

            return money;
        }
    }
}