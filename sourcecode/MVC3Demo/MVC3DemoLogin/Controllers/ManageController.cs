using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = MVC3DemoModel;
using DemoRepository = MVC3DemoRepository;
using MVC3DemoLogin.AppCode;
using Webdiyer.WebControls.Mvc;

namespace MVC3DemoLogin.Controllers
{
    [LoginActionFilter]
    public class ManageController : Controller
    {
        public ActionResult Main(int? id = 1)
        {
            ViewData["username"] = string.Empty;
            if (Request.QueryString["username"] != null)
            {
                ViewData["username"] = Request.QueryString["username"].ToString();
            }
            List<Model.User> userList = new List<Model.User>();
            int totalCount = 0;
            int pageIndex = id ?? 1;
            userList = DemoRepository.User.GetList(ViewData["username"].ToString(), 2, (pageIndex - 1) * 2, out totalCount);
            PagedList<Model.User> mPage = userList.AsQueryable().ToPagedList(0, 2);
            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(id ?? 1);
            return View(mPage);
        }

        [HttpGet]
        public ActionResult UserDetail(int id)
        {
            //取出用户信息
            if (id != 0)
            {
                Model.User user = DemoRepository.User.Get(new Model.User() { UserID = id });
                ViewBag.Residential = DemoRepository.Address.Get(new Model.Address() { AddressID = Convert.ToInt32(user.Residential) }).Name;
                ViewBag.FloorNo = DemoRepository.Address.Get(new Model.Address() { AddressID = Convert.ToInt32(user.FloorNo) }).Name;
                ViewBag.UnitNo = DemoRepository.Address.Get(new Model.Address() { AddressID = Convert.ToInt32(user.UnitNo) }).Name;
                ViewBag.DoorplateNo = DemoRepository.Address.Get(new Model.Address() { AddressID = Convert.ToInt32(user.DoorplateNo) }).Name;
                return View(user);
            }
            return View();
        }

        public ActionResult UserEdit(int id)
        {
            //取出用户信息
            if (id != 0)
            {
                Model.User user = DemoRepository.User.Get(new Model.User() { UserID = id });

                //取出数据，并通过Helper把数据分解
                AddressHelper addressHelper = AddressHelper.GetInstance();
                addressHelper.GetResidetialItem(GetList());
                //反选并使用ViewBag传到View
                ViewBag.ViewResidential = new SelectList(addressHelper.ResidetialItem, "Value", "Text", user.Residential);
                ViewBag.ViewFloorNo = new SelectList(addressHelper.FloorNoItem, "Value", "Text", user.FloorNo);
                ViewBag.ViewUnitNo = new SelectList(addressHelper.UnitNoItem, "Value", "Text", user.UnitNo);
                ViewBag.ViewDoorplateNo = new SelectList(addressHelper.DoorplateNoItem, "Value", "Text", user.DoorplateNo);
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserEdit(Model.User newUser)
        {
            //取出数据，并通过Helper把数据分解
            AddressHelper addressHelper = AddressHelper.GetInstance();
            addressHelper.GetResidetialItem(GetList());
            //使用ViewBag传到View
            ViewBag.Residential = addressHelper.ResidetialItem;
            ViewBag.FloorNo = addressHelper.FloorNoItem;
            ViewBag.UnitNo = addressHelper.UnitNoItem;
            ViewBag.DoorplateNo = addressHelper.DoorplateNoItem;

            //取出用户信息
            if (newUser.UserID != 0)
            {
                Model.User user = DemoRepository.User.Get(new Model.User() { UserID = newUser.UserID });
                //修改信息
                user.UserName = newUser.UserName;
                user.Phone = newUser.Phone;
                user.Residential = newUser.Residential;
                user.UnitNo = newUser.UnitNo;
                user.FloorNo = newUser.FloorNo;
                user.DoorplateNo = newUser.DoorplateNo;
                DemoRepository.User.Save(user);
                //客户端显示
                MessageBox.ShowAndRedirect(this, "修改成功！", "/Manage/Main/");
            }
            return View();
        }

        //删除用户
        public void UserRemove(int id, FormCollection formCollection)
        {
            Model.User user = new Model.User() { UserID = id };
            DemoRepository.User.Remove(user);
            MessageBox.ShowAndRedirect(this, "删除成功！", "/Manage/Main/");
        }

        //获取用户列表
        private List<Model.Address> GetList()
        {
            List<Model.Address> listAddress = DemoRepository.Address.GetList();
            return listAddress;
        }

    }
}
