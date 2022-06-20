using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ExecuteClass
    {
        public ExecuteClass()
        {
            //constructor 
        }

        DBConnectionTableAdapters.sprCustomQueryTableAdapter ta = new DBConnectionTableAdapters.sprCustomQueryTableAdapter();

        public bool NonSelectQuery(string query, ref string msg)
        {
            try
            {
                ta.ExecuteQuery(query);
                return true;
            }
            catch (Exception e)
            {
                msg = "Source:" + e.Source + "\r\nMessage:" + e.Message;
            }
            return false;
        }

        public DataTable SelectQuery(string query, ref string msg)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = ta.ExecuteQuery(query);
            }
            catch (Exception e)
            {
                msg = "Source:" + e.Source + "\r\nMessage:" + e.Message;
            }
            return dt;
        }

        public string SelectSingleQuery(string query, ref string msg)
        {
            string result = string.Empty;

            try
            {
                DataTable dt = ta.ExecuteQuery(query);
                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0][0].ToString();
                }
                else
                {
                    result = "0";
                }
            }
            catch (Exception e)
            {
                msg = "Source:" + e.Source + "\r\nMessage:" + e.Message;
                result = "0";
            }

            return result;
        }
    }
}