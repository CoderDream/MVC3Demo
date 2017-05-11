using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = MVC3DemoModel;
using HZYT.DBUtility;

namespace MVC3DemoRepository
{
    public class Address
    {
        //获取所有地址列表
        public static List<Model.Address> GetList()
        {
            //Model.User
            return GetList(ORM.GetList(new Model.Address(), new Model.AddressMapping(), Constant.CONNSTRING));
        }

        //获取地址详细信息
        public static Model.Address Get(Model.Address address)
        {
            return (Model.Address)ORM.Get(address, new Model.AddressMapping(), Constant.CONNSTRING);
        }

        #region 类型转换
        /// <summary>
        /// 将对象类型转换成为User类型
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        private static List<Model.Address> GetList(List<object> objList)
        {
            List<Model.Address> addressList = new List<Model.Address>();
            foreach (object tempobject in objList)
            {
                addressList.Add((Model.Address)tempobject);
            }
            return addressList;
        }
        #endregion
    }
}
