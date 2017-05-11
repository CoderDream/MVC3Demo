
using System.Web.Mvc;

namespace MVC3DemoLogin
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        //Action执行前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //检测Cookie
            CookieHelper cookie = new CookieHelper();
            if (!cookie.IsCreate)
                filterContext.HttpContext.Response.Redirect("/User/Login/");
            base.OnActionExecuting(filterContext);
        }
    }
}