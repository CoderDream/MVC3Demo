using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = MVC3DemoModel;
using HZYT.DBUtility;

namespace MVC3DemoRepository
{
    public class User
    {
        //用户登陆
        public static Model.User UserLogin(Model.User user)
        {
            string SQL = string.Format("UserName = '{0}' and Password = '{1}'", user.UserName, user.Password);
            object User = ORM.Get(new Model.User(), new Model.UserMapping(), Constant.CONNSTRING, SQL);
            if (User != null)
                return (Model.User)User;
            else
                return null;
        }

        //添加用户
        public static void Add(Model.User user)
        {
            ORM.Add(user, new Model.UserMapping(), Constant.CONNSTRING);
        }

        //修改用户
        public static void Save(Model.User user)
        {
            ORM.Save(user, new Model.UserMapping(), Constant.CONNSTRING);
        }

        //移除用户
        public static void Remove(Model.User user)
        {
            ORM.Remove(user, new Model.UserMapping(), Constant.CONNSTRING);
        }

        //获取用户信息
        public static Model.User Get(Model.User user)
        {
            return (Model.User)ORM.Get(user, new Model.UserMapping(), Constant.CONNSTRING);
        }

        //获取用户列表
        public static List<Model.User> GetList(string UserName, int PageSize, int CurrentCount, out int TotalCount)
        {
            string where = "1=1";
            if (UserName != string.Empty)
            {
                where += " AND UserName LIKE '%" + UserName + "%'";
            }
            List<object> listObject = ORM.GetList(new Model.User(), new Model.UserMapping(), Constant.CONNSTRING, PageSize, CurrentCount, out TotalCount, where);
            return GetList(listObject);
        }

        #region 类型转换
        /// <summary>
        /// 将对象类型转换成为User类型
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        private static List<Model.User> GetList(List<object> objList)
        {
            List<Model.User> userList = new List<Model.User>();
            foreach (object tempobject in objList)
            {
                userList.Add((Model.User)tempobject);
            }
            return userList;
        }
        #endregion
    }
}
