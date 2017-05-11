using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC3DemoModel
{
    public class AddressMapping
    {
        /// <summary>
        /// 获取表名称
        /// </summary>
        public static string GetTableName()
        {
            return "[Address]";
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        public static Dictionary<string, string> GetIdentityMapping()
        {
            Dictionary<string, string> Identity = new Dictionary<string, string>();
            Identity.Add("AddressID", "AddressID");
            return Identity;
        }

        /// <summary>
        /// 获取基础字段
        /// </summary>
        public static Dictionary<string, string> GetBaseFieldMapping()
        {
            Dictionary<string, string> BaseField = new Dictionary<string, string>();
            BaseField.Add("Name", "Name");
            BaseField.Add("Type", "Type");
            return BaseField;
        }
    }
}
