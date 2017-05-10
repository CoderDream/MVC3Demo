using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3Demo.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //登陆控制器
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]//登陆控制器
        public ActionResult Login(Models.LoginModel loginModel)
        {
            if (loginModel.UserName == "张三" && loginModel.Password == "123456")
                Response.Write("正确！");
            else
                Response.Write("不正确！");
            return View();
        }

        //UserDetail
        public ActionResult UserDetail()
        {
            Models.UserModel userModel = new Models.UserModel();
            userModel.UserName = "用户名";
            userModel.Password = "密码";
            userModel.Sex = 0;
            userModel.Age = 30;
            userModel.Height = 170;
            userModel.Weight = 70;
            userModel.Graduated = "毕业院校";
            return View(userModel);
        }

        //UserEdit
        public ActionResult UserEdit()
        {
            Models.UserModel userModel = new Models.UserModel();
            userModel.UserName = "用户名";
            userModel.Age = 10;
            //其他....
            return View(userModel);
        }

        [HttpPost]//UserEdit
        public ActionResult UserEdit(Models.UserModel userModel)
        {
            Response.Write(userModel.UserName);
            Response.Write("<br />");
            Response.Write(userModel.Password);
            Response.Write("<br />");
            Response.Write(userModel.Sex);
            Response.Write("<br />");
            Response.Write(userModel.Age);
            Response.Write("<br />");
            Response.Write(userModel.Height);
            Response.Write("<br />");
            Response.Write(userModel.Weight);
            Response.Write("<br />");
            Response.Write(userModel.Graduated);
            Response.Write("<br />");
            return View();
        }

        //UserEdit
        public ActionResult UserEdit_01()
        {
            return View();
        }

        [HttpPost]//UserEdit
        public ActionResult UserEdit_01(FormCollection form)
        {
            Response.Write(form["UserName"]);
            Response.Write("<br />");
            Response.Write(form[1]);
            Response.Write("<br />");
            return View();
        }

    }
}
