using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3DemoLogin.Controllers
{
    public class ControllerBase : Controller
    {
        //获取用户编号
        public string UserID
        {
            get
            {
                CookieHelper cookieHelper = new CookieHelper();
                if (!cookieHelper.IsCreate)
                    Response.Redirect("/User/Login/");
                return cookieHelper.GetCookie().Values[0].ToString();
            }
        }

        //获取用户名称
        public string UserName
        {
            get
            {
                CookieHelper cookieHelper = new CookieHelper();
                if (!cookieHelper.IsCreate)
                    Response.Redirect("/User/Login/");
                return cookieHelper.GetCookie().Values[1].ToString();
            }
        }
    }
}
