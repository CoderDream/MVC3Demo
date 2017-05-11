using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MVC3DemoModel
{
    //用户地址
    public class Address
    {
        public int AddressID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
