using System;
using System.Web.Mvc;

namespace Mercedes.Web.Framework.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //var workContext = new WorkContext();
            //bool isAuthenticated = workContext.CurrentUser != null;

            //if (!isAuthenticated)
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}
