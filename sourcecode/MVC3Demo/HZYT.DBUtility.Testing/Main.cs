using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HZYT.Model;
using HZYT.DBUtility;
using System.Configuration;

namespace HZTY.DBUtility.Testing
{
    public class Main
    {
        string STR_CONNSTRING, STR_ASSEMBLYPATH;

        public Main()
        {

            STR_CONNSTRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            STR_ASSEMBLYPATH = ConfigurationManager.AppSettings["Model"];
        }


        public void AddTest()
        {
            User user = new User();
            user.StrName = "Test";
            user.StrDiscribe = "Dis";
            user.StrPassword = "123456";
            ORM.Add(user, STR_ASSEMBLYPATH, STR_CONNSTRING);
            int intMaxID = 0;
            ORM.Add(user, out intMaxID, STR_ASSEMBLYPATH, STR_CONNSTRING);
            int temp = intMaxID;
        }

        public void SaveTest()
        {
            User user = new User();
            user.IntID = 12;
            user = (User)ORM.Get(user, STR_ASSEMBLYPATH, STR_CONNSTRING);
            user.StrName = "Testa";
            ORM.Save(user, STR_ASSEMBLYPATH, STR_CONNSTRING);
        }

        public void RemoveTest()
        {
            User user = new User();
            user.IntID = 12;
            ORM.Remove(user, STR_ASSEMBLYPATH, STR_CONNSTRING);
        }

        public void GetListTest()
        {
            List<User> listUser = new List<User>();

            List<object> tempList = ORM.GetList(new User(), STR_ASSEMBLYPATH, STR_CONNSTRING);
            foreach (object tempobject in tempList)
            {
                listUser.Add((User)tempobject);
            }
        }
    }
}
