using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.WebPages.Html;
using System.Xml;
using System.Xml.Serialization;

namespace ESAQAApp.Helpers
{
    public static class Extention
    {
        /// <summary>
        /// Converts datatable to list<T> dynamically
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="dataTable">data table to convert</param>
        /// <returns>List<T></returns>
        public static List<T> DataTableToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);
                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {
                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue(classObj, ConvertToDateTime(dataRow[dtField.Name]), null);
                        }
                        if (propertyInfos.PropertyType == typeof(DateTime?))
                        {
                            propertyInfos.SetValue(classObj, ConvertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue(classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int?))
                        {
                            propertyInfos.SetValue(classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue(classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long?))
                        {
                            propertyInfos.SetValue(classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue(classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal?))
                        {
                            propertyInfos.SetValue(classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(bool))
                        {
                            propertyInfos.SetValue(classObj, ConvertToBool(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(bool?))
                        {
                            propertyInfos.SetValue(classObj, ConvertToBool(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(String))
                            {
                                propertyInfos.SetValue(classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                            else if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue(classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else if (dataRow[dtField.Name].GetType() == typeof(DateTime?))
                            {
                                propertyInfos.SetValue(classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }

                dataList.Add(classObj);
            }

            return dataList;
        }

        public static string ConvertToDateString(object date)
        {
            if (date == null)
                return string.Empty;

            return Convert.ToDateTime(date).ToString();
        }

        public static string ConvertToString(object value)
        {
            return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        }

        public static int ConvertToInt(object value)
        {
            return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static long ConvertToLong(object value)
        {
            return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static bool ConvertToBool(object value)
        {
            return Convert.ToBoolean(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static DateTime ConvertToDateTime(object date)
        {
            return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        }


        public static string ConvertByteToString(this byte[] bytes)
        {
            string response = string.Empty;

            foreach (byte b in bytes)
                response += (Char)b;

            return response;
        }

        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }

        public static int ToInt(this string val)
        {
            return int.Parse(val);
        }

        public static Int16 ToInt16(this string val)
        {
            return Int16.Parse(val);
        }

        public static Int32 ToInt32(this string val)
        {
            return Int32.Parse(val);
        }

        public static Int64 ToInt64(this string val)
        {
            return Int64.Parse(val);
        }

        public static Decimal ToDecimal(this string val)
        {
            if (!string.IsNullOrEmpty(val))
                return Decimal.Parse(val);
            else
                return 0;
        }

        public static float ToFloat(this string val)
        {
            return float.Parse(val);
        }

        public static long ToLong(this string val)
        {
            return long.Parse(val);
        }

        public static bool ToBool(this string val)
        {
            return bool.Parse(val);
        }

        public static Boolean ToBoolean(this string val)
        {
            return Boolean.Parse(val);
        }

        //public static string ToDateFormat(this string val)
        //{
        //    DateTime dtResult = DateTime.Parse(val);
        //    return dtResult.ToString("MMM dd, yyyy");
        //}

        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayText), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DisplayText)attrs[0]).Text;
            }

            return en.ToString();
        }

        // requires object instance, but you can skip specifying T
        public static string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of both object type and property type
        public static string GetPropertyName<TObject, TResult>(Expression<Func<TObject, TResult>> exp)
        {
            // extract property name
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of object type
        public static string GetPropertyName<TObject>(Expression<Func<TObject, object>> exp)
        {
            var body = exp.Body;
            var convertExpression = body as UnaryExpression;
            if (convertExpression != null)
            {
                if (convertExpression.NodeType != ExpressionType.Convert)
                {
                    throw new ArgumentException("Invalid property expression.", "exp");
                }
                body = convertExpression.Operand;
            }
            return ((MemberExpression)body).Member.Name;
        }
    }

    public static class HelperFunctions
    {
        public static bool GetResolvedConnecionIpAddress(string serverNameOrUrl, out string resolvedIpAddress)
        {
            var isResolved = false;
            IPAddress resolvIp = null;
            try
            {
                if (!IPAddress.TryParse(serverNameOrUrl, out resolvIp))
                {
                    var hostEntry = Dns.GetHostEntry(serverNameOrUrl);

                    if (hostEntry != null && hostEntry.AddressList != null
                        && hostEntry.AddressList.Length > 0)
                    {
                        if (hostEntry.AddressList.Length == 1)
                        {
                            resolvIp = hostEntry.AddressList[0];
                            isResolved = true;
                        }
                        else
                        {
                            foreach (var var in hostEntry.AddressList.Where(var => var.AddressFamily == AddressFamily.InterNetwork))
                            {
                                resolvIp = var;
                                isResolved = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    isResolved = true;
                }
            }
            catch (Exception)
            {
                isResolved = false;
                resolvIp = null;
            }
            finally
            {
                if (resolvIp != null) resolvedIpAddress = resolvIp.ToString();
            }

            resolvedIpAddress = null;
            return isResolved;
        }

        public static string SerializeObject<T>(T source)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                serializer.Serialize(writer, source);
                return sw.ToString();
            }
        }

        public static T DeSerializeObject<T>(string xml)
        {
            using (var sr = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        public static object ReturnZeroIfNull(this object value)
        {
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }

        public static object ReturnEmptyIfNull(this object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        public static object ReturnFalseIfNull(this object value)
        {
            if (value == DBNull.Value)
                return false;
            if (value == null)
                return false;
            return value;
        }

        public static object ReturnDateTimeMinIfNull(this object value)
        {
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public static object ReturnNullIfDbNull(this object value)
        {
            if (value == DBNull.Value)
                return '\0';
            if (value == null)
                return '\0';
            return value;
        }

        //This function formats the display-name of a user,
        //and removes unnecessary extra information.
        public static string FormatUserDisplayName(string displayName = null, string defaultValue = "tBill Users",
            bool returnNameIfExists = false, bool returnAddressPartIfExists = false)
        {
            //Get the first part of the Users's Display Name if s/he has a name like this: "firstname lastname (extra text)"
            //removes the "(extra text)" part
            if (!string.IsNullOrEmpty(displayName))
            {
                if (returnNameIfExists)
                    return Regex.Replace(displayName, @"\ \(\w{1,}\)", "");
                return (displayName.Split(' '))[0];
            }
            if (returnAddressPartIfExists)
            {
                var emailParts = defaultValue.Split('@');
                return emailParts[0];
            }
            return defaultValue;
        }

        public static string FormatUserTelephoneNumber(this string telephoneNumber)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(telephoneNumber))
            {
                //result = telephoneNumber.ToLower().Trim().Trim('+').Replace("tel:", "");
                result = telephoneNumber.ToLower().Trim().Replace("tel:", "");

                if (result.Contains(";"))
                {
                    if (!result.ToLower().Contains(";ext="))
                        result = result.Split(';')[0];
                }
            }

            return result;
        }

        public static bool IsValidEmail(this string emailAddress)
        {
            const string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(emailAddress, pattern);
        }

        /// <summary>
        /// Convert DateTime to string
        /// </summary>
        /// <param name="datetTime"></param>
        /// <param name="excludeHoursAndMinutes">if true it will execlude time from datetime string. Default is false</param>
        /// <returns></returns>
        public static string ConvertDate(this DateTime datetTime, bool excludeHoursAndMinutes = false)
        {
            if (datetTime != DateTime.MinValue)
            {
                if (excludeHoursAndMinutes)
                    return datetTime.ToString("yyyy-MM-dd");
                return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return null;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public static string ConvertSecondsToReadable(this int secondsParam)
        {
            var hours = Convert.ToInt32(Math.Floor((double)(secondsParam / 3600)));
            var minutes = Convert.ToInt32(Math.Floor((double)(secondsParam - (hours * 3600)) / 60));
            var seconds = secondsParam - (hours * 3600) - (minutes * 60);

            var hoursStr = hours.ToString();
            var minsStr = minutes.ToString();
            var secsStr = seconds.ToString();

            if (hours < 10)
            {
                hoursStr = "0" + hoursStr;
            }

            if (minutes < 10)
            {
                minsStr = "0" + minsStr;
            }
            if (seconds < 10)
            {
                secsStr = "0" + secsStr;
            }

            return hoursStr + ':' + minsStr + ':' + secsStr;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public static string ConvertSecondsToReadable(this long secondsParam)
        {
            var hours = Convert.ToInt32(Math.Floor((double)(secondsParam / 3600)));
            var minutes = Convert.ToInt32(Math.Floor((double)(secondsParam - (hours * 3600)) / 60));
            var seconds = Convert.ToInt32(secondsParam - (hours * 3600) - (minutes * 60));

            var hoursStr = hours.ToString();
            var minsStr = minutes.ToString();
            var secsStr = seconds.ToString();

            if (hours < 10)
            {
                hoursStr = "0" + hoursStr;
            }

            if (minutes < 10)
            {
                minsStr = "0" + minsStr;
            }
            if (seconds < 10)
            {
                secsStr = "0" + secsStr;
            }

            return hoursStr + ':' + minsStr + ':' + secsStr;
        }
    }

    public class DisplayText : Attribute
    {
        public DisplayText(string Text)
        {
            this.text = Text;
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }

    public class EnumCollection
    {
        public enum OnePointTwo
        {
            //[DisplayText("Within ESA")]
            Within_ESA,

            //[DisplayText("Use for Agriculture")]
            Use_for_Agriculture,

            //[DisplayText("Use for Fisheries")]
            Use_for_Fisheries,

            //[DisplayText("Exist Community Park")]
            Exist_Community_Park,

            //[DisplayText("Exist Social Forestry")]
            Exist_Social_Forestry,

            //[DisplayText("Available Water Bodies (Fresh)")]
            Fresh_Water_Bodies,

            //[DisplayText("Hospital")]
            Hospital,

            //[DisplayText("Industrial Use")]
            Industrial_Use
        }

        public enum ThreePointTwo
        {
            //[DisplayText("Mild")]
            Mild,

            //[DisplayText("Dense")]
            Dense,

            //[DisplayText("Crucial")]
            Crucial
        }

        public enum ThreePointThree
        {
            //[DisplayText("Industry")]
            Industry,

            //[DisplayText("Growth Center")]
            Growth_Center,

            //[DisplayText("Other")]
            Other
        }

        public enum SevenPointTwo
        {
            //[DisplayText("Industry")]
            Industry,

            //[DisplayText("Growth Center")]
            Growth_Center,

            //[DisplayText("Bazar")]
            Bazar,

            //[DisplayText("Other")]
            Other
        }

        public enum EightPointTwo
        {
            //[DisplayText("Agriculture")]
            Agriculture,

            //[DisplayText("Construction")]
            Construction,

            //[DisplayText("Food")]
            Food,

            //[DisplayText("Hospitality")]
            Hospitality,

            //[DisplayText("Motor")]
            Motor,

            //[DisplayText("Hospitality")]
            Transportation,

            //[DisplayText("Other")]
            Other
        }

        public enum EightPointFour
        {
            //[DisplayText("Solid")]
            Solid,

            //[DisplayText("Liquid")]
            Liquid,

            //[DisplayText("Gas")]
            Gas,

            //[DisplayText("Other")]
            Other
        }

        public enum Nine
        {
            //[DisplayText("Far")]
            Far,

            //[DisplayText("Near")]
            Near
        }

        public enum Ten
        {
            //[DisplayText("Upstream")]
            Upstream,

            //[DisplayText("Downstream")]
            Downstream
        }

        public enum TwelvePointTwo
        {
            //[DisplayText("Agriculture")]
            WASA_Sewerage,

            //[DisplayText("Fisheries")]
            Untreated_Drainage_Water,

            //[DisplayText("Open_Defecation")]
            Open_Defecation,

            //[DisplayText("Industrial_Waste")]
            Industrial_Waste,

            //[DisplayText("Effluent")]
            Effluent
        }

        public enum UsesOfWater
        {
            //[DisplayText("Agriculture")]
            Agriculture,

            //[DisplayText("Fisheries")]
            Fisheries,

            //[DisplayText("Households")]
            Households,

            //[DisplayText("Industrial")]
            Industrial,

            //[DisplayText("Unused")]
            Unused
        }

        //public List<SelectListItem> EightPointTwo
        //{
        //    get
        //    {
        //        List<SelectListItem> EightPointTwo = new List<SelectListItem>();
        //        EightPointTwo.Add(new SelectListItem { Text = "Agriculture & Forestry/ Wildlife", Value = "1" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Business & Information", Value = "2" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Construction/ Utilities/ Contracting", Value = "3" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Education", Value = "4" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Finance & Insurance", Value = "5" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Food & Hospitality", Value = "6" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Gaming", Value = "7" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Health Services", Value = "8" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Motor Vehicle", Value = "9" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Natural Resources/ Environmental", Value = "10" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Other", Value = "11" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Personal Services", Value = "12" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Real Estate & Housing", Value = "13" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Safety/ Security & Legal", Value = "14" });
        //        EightPointTwo.Add(new SelectListItem { Text = "Transportation", Value = "15" });
        //        return EightPointTwo;
        //    }

        //    private set { }
        //}
    }
}