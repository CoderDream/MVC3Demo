using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HZYT.DBUtility
{
    public class Model
    {
        /// <summary>
        /// 获取字段名称及值的字典
        /// </summary>
        /// <param name="classObject">对象类型</param>
        /// <param name="FieldMapping">字段</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetValueMapping(object classObject, Dictionary<string, string> FieldMapping)
        {
            Dictionary<string, string> FieldValueMapping = new Dictionary<string, string>();
            foreach (string strKey in FieldMapping.Keys)
            {
                //获取属性的值
                object arrObject = classObject.GetType().InvokeMember(strKey, BindingFlags.GetProperty, null, classObject, null);
                if (arrObject != null)
                {
                    FieldValueMapping.Add(FieldMapping[strKey], arrObject.ToString());
                }
            }
            return FieldValueMapping;
        }


        /// <summary>
        /// 转换成为指定的数据类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objvalee"></param>
        /// <returns></returns>
        internal static object[] GetProType(Type type, object objectValue)
        {

            //如果属性不存在，返回 null
            if (objectValue == null)
                return null;

            if (type == typeof(int))
            {
                return new object[] { Convert.ToInt32(objectValue) };
            }
            if (type == typeof(double))
            {
                return new object[] { Convert.ToDouble(objectValue) };
            }
            if (type == typeof(decimal))
            {
                return new object[] { Convert.ToDecimal(objectValue) };
            }
            else if (type == typeof(string))
            {
                return new string[] { objectValue.ToString() };
            }
            else if (type == typeof(byte))
            {
                return new object[] { Convert.ToByte(objectValue) };
            }
            else if (type == typeof(DateTime))
            {
                return new object[] { Convert.ToDateTime(objectValue) };
            }
            else
            {
                return new string[] { objectValue.ToString() };
            }
        }

    }
}
