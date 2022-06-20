using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BAC007.Helpers;

namespace BAC007.Controllers
{
    public class BaseController : Controller
    {
        private HttpSessionStateBase _session;
        protected HttpSessionStateBase CrossControllerSession
        {
            get
            {
                if (_session == null) _session = Session;
                return _session;
            }
            set
            {
                _session = Session;
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        public enum Month
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public enum Sign
        {
            Danger,
            Dark,
            Default,
            Error,
            Info,
            Primary,
            Success,
            Warning
        }

        public enum OperationMessage
        {
            [DisplayText("Information has been deleted.")]
            Delete,

            [DisplayText("Information not deleted.")]
            NotDelete,

            [DisplayText("An error has been occured!")]
            Error,

            [DisplayText("Data has been saved successfully.")]
            Success,

            [DisplayText("Data not save.")]
            NotSuccess,

            [DisplayText("Data has been updated successfully.")]
            Update,

            [DisplayText("Data not updated.")]
            NotUpdate,

            [DisplayText("Data has been imported successfully.")]
            ImportSuccess,

            [DisplayText("Data not imported successfully.")]
            ImportNotSuccess
        }

        public string ShowMessage(Enum val, string msg)
        {
            return val + ": " + msg;
        }

        public string ExtractInnerException(Exception ex)
        {
            string message = string.Empty;
            if (ex.Message.Contains("See the inner exception"))
            {
                if (ex.InnerException.Message.Contains("See the inner exception"))
                {
                    message = ex.InnerException.InnerException.Message;
                }
                else
                {
                    message = ex.InnerException.Message.ToString();
                }
            }
            else
            {
                message = ex.Message.ToString();
            }

            return message.Replace("'", "").Replace("\"", "").Replace("\r\n", "").Replace("\n", "");
        }

        public enum ReportType : int
        {
            //Line = 1,
            //Bar = 2,
            //Pie = 3,
            //Table = 4
            Graphical = 1,
            Time = 2
        }

        public Dictionary<string, string> Operator()
        {
            var dictionary = new Dictionary<string, string> { { "equal [=]", "=" }, { "not equal [!=]", "!=" }, { "greater than [>]", ">" }, { "greater than or equal [>=]", ">=" }, { "less than [<]", "<" }, { "less than or equal [<=]", "<=" } };

            return dictionary;
        }

        public enum WeatherCondition
        {
            [DisplayText("Sunny")]
            Sunny,

            [DisplayText("Half-Sunny")]
            HalfSunny,

            [DisplayText("Cloudy/Foggy")]
            CloudyFoggy,

            [DisplayText("Half-Rainy")]
            HalfRainy,

            [DisplayText("Rainy")]
            Rainy
        }
    }
}