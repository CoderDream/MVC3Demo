using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HZYT.Model
{
    public class UserMapping
    {
        /// <summary>
        /// 获取表名称
        /// </summary>
        public static string GetTableName()
        {
            return "tbUser";
        }



        /// <summary>
        /// 获取主键
        /// </summary>
        public static Dictionary<string, string> GetIdentityMapping()
        {
            Dictionary<string, string> Identity = new Dictionary<string, string>();
            Identity.Add("IntID", "intID");
            return Identity;
        }



        /// <summary>
        /// 获取基础字段
        /// </summary>
        public static Dictionary<string, string> GetBaseFieldMapping()
        {
            Dictionary<string, string> BaseField = new Dictionary<string, string>();
            BaseField.Add("StrName", "varName");
            BaseField.Add("StrDiscribe", "varDiscribe");
            BaseField.Add("StrPassword", "varPassword");
            return BaseField;
        }
    }
}
