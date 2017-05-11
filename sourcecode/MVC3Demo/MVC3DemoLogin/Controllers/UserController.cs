using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = MVC3DemoModel;
using Repository = MVC3DemoRepository;
using MVC3DemoLogin.AppCode;

namespace MVC3DemoLogin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //登陆窗体
        public ActionResult Login()
        {
            CookieHelper cookieHelper = new CookieHelper();
            if (cookieHelper.IsCreate != false)
                Response.Redirect("/Manage/Main/");
            return View();
        }

        [HttpGet]
        public void Logout()
        {
            CookieHelper cookieHelper = new CookieHelper();
            cookieHelper.ClearCookie();
            Response.Redirect("/User/Login/");
        }

        [HttpPost]//登陆时回发处理
        public ActionResult Login(Model.User user)
        {
            if (ModelState["UserName"].Errors.Count == 0 && ModelState["Password"].Errors.Count == 0)
            {
                Model.User newUser = Repository.User.UserLogin(user);
                //检测用户名和密码
                if (newUser != null)
                {
                    DateTime Expires = DateTime.Now;
                    if (user.Remember == true)
                        Expires = DateTime.Now.AddDays(365);
                    else
                        Expires = DateTime.Now.AddDays(1);

                    Dictionary<string, string> CookieValues = new Dictionary<string, string>();
                    CookieValues.Add("UserID", newUser.UserID.ToString());
                    CookieValues.Add("UserName", newUser.UserName);
                    CookieHelper cookieHelper = new CookieHelper();

                    cookieHelper.SetCookie(CookieValues, Expires);
                    Response.Redirect("/Manage/Main");
                }
                else
                {
                    MessageBox.Show(this, "用户名或密码不正确！");
                }
            }
            //客户端显示
            return View();
        }

        //用户注册
        public ActionResult Regedit()
        {
            //取出数据，并通过Helper把数据分解
            AddressHelper addressHelper = AddressHelper.GetInstance();
            addressHelper.GetResidetialItem(GetList());
            //使用ViewBag传到View
            ViewBag.Residential = addressHelper.ResidetialItem;
            ViewBag.FloorNo = addressHelper.FloorNoItem;
            ViewBag.UnitNo = addressHelper.UnitNoItem;
            ViewBag.DoorplateNo = addressHelper.DoorplateNoItem;
            return View();
        }

        [HttpPost]//注册时处理回发
        public ActionResult Regedit(Model.User user, FormCollection form)
        {
            //取出数据，并通过Helper把数据分解
            AddressHelper addressHelper = AddressHelper.GetInstance();
            addressHelper.GetResidetialItem(GetList());
            //使用ViewBag传到View
            ViewBag.Residential = addressHelper.ResidetialItem;
            ViewBag.FloorNo = addressHelper.FloorNoItem;
            ViewBag.UnitNo = addressHelper.UnitNoItem;
            ViewBag.DoorplateNo = addressHelper.DoorplateNoItem;

            //校验验证码
            if (form["checkCode"] != null && form["checkCode"].ToString() == Session["CheckCode"].ToString())
            {
                //校验其他表单元素
                if (ModelState.IsValid)
                {
                    Repository.User.Add(user);
                    MessageBox.ShowAndRedirect(this, "注册成功，请登陆！", "/User/Login");
                }
            }
            else
            {
                MessageBox.Show(this, "验证码不正确！");
            }
            return View();
        }

        //获取用户列表
        private List<Model.Address> GetList()
        {
            List<Model.Address> listAddress = Repository.Address.GetList();
            return listAddress;
        }

    }
}
