using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MVC3DemoRepository
{
    class Constant
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    }
}
