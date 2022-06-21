using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ESAQAApp.Helpers
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        public string AccessDeniedController { get; set; }
        public string AccessDeniedAction { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (String.IsNullOrWhiteSpace(AccessDeniedController) || String.IsNullOrWhiteSpace(AccessDeniedAction))
                {
                    AccessDeniedController = "Account";
                    AccessDeniedAction = "Login";
                }

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Controller = AccessDeniedController,
                            Action = AccessDeniedAction
                        }));

                return;
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                AccessDeniedController = "Account";
                AccessDeniedAction = "Denied";

                //filterContext.Result = new RedirectResult("~/Account/Denied");

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Controller = AccessDeniedController,
                            Action = AccessDeniedAction,
                        }));

                return;
            }

            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                AccessDeniedController = "Account";
                AccessDeniedAction = "Denied";

                //filterContext.Result = new RedirectResult("~/Account/Denied");

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            Controller = AccessDeniedController,
                            Action = AccessDeniedAction,
                        }));

                return;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        public byte Precision { get; set; }
        public byte Scale { get; set; }

    }
}