
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2012-01-02
 *
 * 描  述：orm与数据库交互类
 *
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace HZYT.DBUtility
{
    public class SQL
    {
        /// <summary>
        /// 创建Insert语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名</param>
        /// <param name="IdentityMapping">主键映射字典</param>
        /// <param name="FieldMapping">字段映射字典</param>
        /// <param name="FieldValueMapping">属性-值字典</param>
        /// <param name="intFlag">Insert语句</param>
        /// <returns></returns>
        internal static string CreateInsert(object classObject, string strTableName, Dictionary<string, string> IdentityMapping, Dictionary<string, string> FieldMapping, Dictionary<string, string> FieldValueMapping, int intFlag)
        {
            //声明字段列表
            List<string> listFiledList = new List<string>();
            //声明值列表
            List<string> listValueList = new List<string>();
            //为列表添加元素
            foreach (string strKey in FieldMapping.Keys)
            {
                if (FieldValueMapping.ContainsKey(FieldMapping[strKey]))
                {
                    //获取字段名称
                    listFiledList.Add(FieldMapping[strKey]);
                    //获取字段值
                    listValueList.Add(GetFieldValue(classObject, strKey, FieldValueMapping[FieldMapping[strKey]]));
                }
            }
            //把列表转换成为字符串
            string strFieldText = string.Join(",", listFiledList);
            string strValueText = string.Join(",", listValueList);
            string strSql = "INSERT INTO " + strTableName + " (" + strFieldText + ") VALUES (" + strValueText + ")";

            if (intFlag != 0)
            {
                strSql += " SELECT MAX(" + GetIdentity(IdentityMapping) + ") FROM " + strTableName;
            }
            return strSql;
        }

        /// <summary>
        /// 执行Insert语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="intMaxID">返回最大的编号</param>
        /// <param name="intFlag">是否需要返回最大的编号，0为不需要，1为需要</param>
        /// <returns></returns>
        internal static bool ExecInsert(string strSQL, out int intMaxID, int intFlag, string strConnString)
        {
            intMaxID = 0;

            //不需要取出最大编号，执行ExecNnoQuery
            if (intFlag == 0)
            {
                SqlHelper.ExecuteNonQuery(strConnString, CommandType.Text, strSQL);
                return true;
            }
            else//取出最大编号，执行ExecScalar
            {
                intMaxID = Convert.ToInt32(SqlHelper.ExecuteScalar(strConnString, CommandType.Text, strSQL));
                return true;
            }
        }

        /// <summary>
        /// 创建Update语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名</param>
        /// <param name="IdentityMapping">主键映射字典</param>
        /// <param name="FieldMapping">字段映射字典</param>
        /// <param name="FieldValueMapping">属性-值字典</param>
        /// <param name="intFlag">Update语句</param>
        /// <returns></returns>
        internal static string CreateUpdate(object classObject, string strTableName, Dictionary<string, string> IdentityMapping, Dictionary<string, string> FieldMapping, Dictionary<string, string> FieldValueMapping)
        {
            //声明字段列表
            List<string> listUpdateList = new List<string>();
            //为列表添加元素
            foreach (string strKey in FieldMapping.Keys)
            {
                //获取字段值
                listUpdateList.Add(FieldMapping[strKey] + "=" + GetFieldValue(classObject, strKey, FieldValueMapping[FieldMapping[strKey]]));
            }
            //把列表转换成为字符串
            string strUpdateText = string.Join(",", listUpdateList);
            string strSql = "UPDATE " + strTableName + " SET " + strUpdateText + " WHERE " + GetIdentity(IdentityMapping) + " = " + GetIndetityValue(classObject, IdentityMapping);

            return strSql;
        }

        /// <summary>
        /// 执行Insert语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        internal static bool ExecUpdate(string strSQL, string strConnString)
        {
            SqlHelper.ExecuteNonQuery(strConnString, CommandType.Text, strSQL);
            return true;
        }

        /// <summary>
        /// 创建Select语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名</param>
        /// <param name="IdentityMapping">主键映射字典</param>
        /// <param name="FieldMapping">字段映射字典</param>
        /// <param name="FieldValueMapping">属性-值字典</param>
        /// <returns>Select语句</returns>
        internal static string CreateSelect(object classObject, string strTableName, Dictionary<string, string> IdentityMapping, Dictionary<string, string> FieldMapping)
        {
            //声明字段列表
            List<string> listSelectList = new List<string>();
            //为列表添加元素
            foreach (string strKey in FieldMapping.Keys)
            {
                //获取字段值
                listSelectList.Add(FieldMapping[strKey]);
            }
            //把列表转换成为字符串
            string strSelectText = string.Join(",", listSelectList);

            string strSql = "SELECT " + strSelectText + " FROM " + strTableName + " WHERE " + GetIdentity(IdentityMapping) + " = " + GetIndetityValue(classObject, IdentityMapping);

            return strSql;
        }

        /// <summary>
        /// 创建Select语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名</param>
        /// <param name="IdentityMapping">主键映射字典</param>
        /// <param name="FieldMapping">字段映射字典</param>
        /// <param name="FieldValueMapping">属性-值字典</param>
        /// <returns>Select语句</returns>
        internal static string CreateSelect(object classObject, string strTableName, string strWHERE, Dictionary<string, string> FieldMapping)
        {
            //声明字段列表
            List<string> listSelectList = new List<string>();
            //为列表添加元素
            foreach (string strKey in FieldMapping.Keys)
            {
                //获取字段值
                listSelectList.Add(FieldMapping[strKey]);
            }
            //把列表转换成为字符串
            string strSelectText = string.Join(",", listSelectList);

            string strSql = "SELECT " + strSelectText + " FROM " + strTableName + " WHERE 1=1 AND " + strWHERE;

            return strSql;
        }

        /// <summary>
        /// 执行Select语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        internal static object ExecSelect(string strSQL, object classObject, Dictionary<string, string> FieldMapping, string strConnString)
        {
            DataSet ds = SqlHelper.ExecuteDataset(strConnString, CommandType.Text, strSQL);
            List<object> objList = ChangeDateSetToList(classObject, FieldMapping, ds);
            if (objList.Count > 0)
            {
                return objList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建Delete语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="IdentityMapping">主键字典</param>
        /// <returns></returns>
        internal static string CreateDelete(object classObject, string strTableName, Dictionary<string, string> IdentityMapping)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE " + GetIdentity(IdentityMapping) + " = " + GetIndetityValue(classObject, IdentityMapping);
            return strSql;
        }

        /// <summary>
        /// 执行Delete语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        internal static bool ExecDelete(string strSQL, string strConnString)
        {
            SqlHelper.ExecuteNonQuery(strConnString, CommandType.Text, strSQL);
            return true;
        }

        /// <summary>
        /// 创建Delete语句
        /// </summary>
        /// <param name="classObject">对象列表</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="IdentityMapping">主键字典</param>
        /// <returns></returns>
        internal static string CreateDeleteList(List<object> listClassObject, string strTableName, Dictionary<string, string> IdentityMapping)
        {
            StringBuilder sbSQL = new StringBuilder();
            foreach (object classObject in listClassObject)
            {
                sbSQL.Append("DELETE FROM " + strTableName + " WHERE " + GetIdentity(IdentityMapping) + " = " + GetIndetityValue(classObject, IdentityMapping));
            }
            return sbSQL.ToString();
        }

        /// <summary>
        /// 创建SelectList语句
        /// </summary>
        /// <param name="classObject">对象</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="FieldMapping">所有字段字典</param>
        /// <param name="strWHERE">条件，不用加 WHERE</param>
        /// <returns></returns>
        internal static string CreateSelectList(object classObject, string strTableName, Dictionary<string, string> FieldMapping, string strWHERE)
        {
            //声明字段列表
            List<string> listSelectList = new List<string>();
            //为列表添加元素
            foreach (string strKey in FieldMapping.Keys)
            {
                //获取字段值
                listSelectList.Add(FieldMapping[strKey]);
            }
            //把列表转换成为字符串
            string strSelectText = string.Join(",", listSelectList);

            string strSql = "SELECT " + strSelectText + " FROM " + strTableName + " WHERE " + strWHERE;

            return strSql;
        }

        /// <summary>
        /// 执行SelectList语句
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <returns></returns>
        internal static List<object> ExecSelectList(string strSQL, object classObject, Dictionary<string, string> FieldMapping, string ConnString)
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConnString, CommandType.Text, strSQL);
            return ChangeDateSetToList(classObject, FieldMapping, ds);
        }

        /// <summary>
        /// 获取主键名称
        /// </summary>
        /// <param name="IdentityMapping">主键字典</param>
        /// <returns></returns>
        internal static string GetIdentity(Dictionary<string, string> IdentityMapping)
        {
            string strIdentityName = string.Empty;
            foreach (string strKey in IdentityMapping.Keys)
            {
                //取出数据库主键的名称
                strIdentityName = IdentityMapping[strKey];
            }
            return strIdentityName;
        }

        /// <summary>
        /// 取出主键的值
        /// </summary>
        /// <param name="classObject"></param>
        /// <param name="IdentityMapping"></param>
        /// <returns></returns>
        internal static string GetIndetityValue(object classObject, Dictionary<string, string> IdentityMapping)
        {
            //获取传入的实体的类型
            Type type = classObject.GetType();

            //声名变量
            string strIdentityName = string.Empty;
            string objIndetityValue = string.Empty;
            foreach (string strKey in IdentityMapping.Keys)
            {
                //取出数据库主键的名称
                strIdentityName = IdentityMapping[strKey];
                //取出主键的值
                objIndetityValue = type.InvokeMember(strKey, BindingFlags.GetProperty, null, classObject, null).ToString();
            }

            return objIndetityValue;
        }


        /// <summary>
        /// 把DataSet 转换成为List
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="type"></param>
        /// <param name="BaseFieldMapping"></param>
        /// <returns></returns>
        internal static List<object> ChangeDateSetToList(object classObject, Dictionary<string, string> FieldMapping, DataSet ds)
        {
            //获取传入的类型
            Type type = classObject.GetType();
            //创建返回值
            List<object> tempList = new List<object>();

            //检查DataSet的Table和Rows是否存在，至少取出一条数据
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //获取属性名称及类型字典
                PropertyInfo[] properInfo = type.GetProperties();
                Dictionary<string, Type> proDictionary = new Dictionary<string, Type>();
                foreach (PropertyInfo proper in properInfo)
                {
                    proDictionary.Add(proper.Name, proper.PropertyType);
                }

                //遍历DataSet
                int intRowCount = ds.Tables[0].Rows.Count;
                for (int j = 0; j < intRowCount; j++)
                {
                    //创建一个传入类型的新的实例
                    object typeTempObject = type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
                    //属性赋值
                    foreach (string strKey in FieldMapping.Keys)
                    {
                        //根据数据类型取出值
                        object[] arrObject = Model.GetProType(classObject.GetType().GetProperty(strKey).PropertyType, ds.Tables[0].Rows[j][FieldMapping[strKey]].ToString());
                        //属性赋值
                        type.InvokeMember(strKey, BindingFlags.SetProperty, null, typeTempObject, arrObject);
                    }
                    //将实体添加到 List 
                    tempList.Add(typeTempObject);
                    //清空对象引用
                    typeTempObject = null;
                }
            }
            return tempList;
        }

        /// <summary>
        /// 根据数据类型判断是否添加 " ' " ，数据库中的字符型和日期型要加单引号
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objvalee"></param>
        /// <returns></returns>
        internal static string GetFieldValue(object classObject, string strKey, string FieldValue)
        {
            //获取类中的属性的类型
            PropertyInfo properInfo = classObject.GetType().GetProperty(strKey);
            Type type = properInfo.PropertyType;
            //如果属性不存在，返回 null
            if (FieldValue == null)
                return null;
            //根据属性的类型决定是否加“双引号”
            if ((type == typeof(int) || type == typeof(byte)))
            {
                return FieldValue;
            }
            else if (type == typeof(string))
            {
                return "'" + FieldValue + "'";
            }
            else if (type == typeof(DateTime))
            {
                return "'" + FieldValue + "'";
            }
            else
            {
                return null;
            }
        }
    }
}
