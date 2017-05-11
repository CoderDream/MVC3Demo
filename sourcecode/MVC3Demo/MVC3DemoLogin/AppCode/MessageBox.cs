
/*
 * 
 * 创建人：李林峰
 * 
 * 时  间：2009-05-04
 * 
 * 描  述：弹出对话框类
 *  
 */

using System;
using System.Text;

namespace MVC3DemoLogin.Controllers
{
    /// <summary>
    /// Web应用程序脚本输出对话框
    /// </summary>
    public class MessageBox
    {
        private MessageBox()
        {
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.Mvc.Controller page, string msg)
        {
            page.Response.Write("<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }


        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.Mvc.Controller page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("location.href='{0}'", url);
            Builder.Append("</script>");
            page.Response.Write(Builder.ToString());
        }
    }
}
