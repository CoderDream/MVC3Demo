using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3Demo.AppCode
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public string LogMessage { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write(@"在Action执行之前执行" + LogMessage + "<br />");
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(@"在Action执行之后执行" + LogMessage + "<br />");
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write(@"在Result执行之前执行" + LogMessage + "<br />");
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(@"在Result执行之后执行" + LogMessage + "<br />");
            base.OnResultExecuted(filterContext);
        }
    }
}