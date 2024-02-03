using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PimireWebApp.Models;

namespace PimireWebApp.Utilities
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = SessionHelper.GetObjectFromJson<User>(filterContext.HttpContext.Session, "UserProfile");
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Authenticate/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
