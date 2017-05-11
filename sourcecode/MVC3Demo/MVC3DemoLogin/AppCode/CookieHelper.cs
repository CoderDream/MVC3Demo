using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MVC3DemoLogin
{
    public class CookieHelper
    {
        private string name = "User";   //Cookie名称

        //是否已经被创建
        public bool IsCreate
        {
            get
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies[this.name];
                if (Cookie != null)
                    return true;
                else
                    return false;
            }
        }

        //设置Cookies
        public void SetCookie(Dictionary<string, string> Values, DateTime Expires)
        {
            HttpCookie Cookie = new HttpCookie(this.name);
            foreach (string key in Values.Keys)
            {
                Cookie.Values.Add(key, Values[key]);
            }
            Cookie.Expires = Expires;
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        //获取Cookie
        public HttpCookie GetCookie()
        {
            return HttpContext.Current.Request.Cookies[this.name];
        }

        //清空Cookie
        public void ClearCookie()
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[this.name];
            Cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }
    }
}