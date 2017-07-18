using System;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Framework.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthenticated =  null != HttpContext.Current.Session["AccAdmin"];

            if (!isAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult("Admin_Login", null);
            }
        }
    }
}