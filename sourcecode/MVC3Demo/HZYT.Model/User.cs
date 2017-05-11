using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HZYT.Model
{
    public class User
    {
        private int intID;

        public int IntID
        {
            get { return intID; }
            set { intID = value; }
        }

        private string strName;

        public string StrName
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strDiscribe;

        public string StrDiscribe
        {
            get { return strDiscribe; }
            set { strDiscribe = value; }
        }

        private string strPassword;

        public string StrPassword
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
    }
}
