
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2012-01-05
 *
 * 描  述：页面基类
 *
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace HZYT.Common
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        //在配置文件中获取转向页地址
        private static string LOGINPAGE = ConfigurationManager.AppSettings["Login"];

        private int cookieExpires;

        /// <summary>
        /// Cookie过期时间
        /// </summary>
        public int CookieExpires
        {
            get
            {
                if (cookieExpires == 0)
                    return 1;
                else
                    return this.cookieExpires;
            }
            set
            {
                cookieExpires = value;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["UserID"] != null)
                {
                    return int.Parse(HttpContext.Current.Request.Cookies["UserID"].Value);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                //设置值的时候，向cookie里赋值
                HttpCookie cookieid = new HttpCookie("UserID");
                cookieid.Value = value.ToString();
                cookieid.Expires = DateTime.Now.AddDays(cookieExpires);
                HttpContext.Current.Response.Cookies.Add(cookieid);
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["UserName"] != null)
                {
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["UserName"].Value);
                }
                else
                {
                    return "连接超时，请重新登陆！";
                }
            }
            set
            {
                //设置值的时候，向cookie里赋值
                HttpCookie cookiename = new HttpCookie("UserName");
                cookiename.Value = HttpUtility.UrlEncode(value.ToString());
                cookiename.Expires = DateTime.Now.AddDays(cookieExpires);
                HttpContext.Current.Response.Cookies.Add(cookiename);
            }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company
        {
            get
            {
                return ConfigurationManager.AppSettings["Company"];
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        protected void Logout()
        {
            //清空编号Cookie
            HttpCookie cookieid = new HttpCookie("UserID");
            cookieid.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookieid);
            //清空用户名Cookie
            HttpCookie cookiename = new HttpCookie("UserName");
            cookiename.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookiename);
            Response.Write("<script>this.window.parent.location.href='" + LOGINPAGE + "'</script>");
        }

        /// <summary>
        /// 重写页面初始化方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (UserID == 0)
            {
                Response.Write("<script>alert('连接超时，请重新登陆！');this.window.parent.location.href='" + LOGINPAGE + "'</script>");
            }
        }
    }
}
