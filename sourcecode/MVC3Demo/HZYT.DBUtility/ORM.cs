
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2010-01-10第一版本，2011-12-31第一次重构，2012年7月9日把ChangeDateSetToList接口开放
 *
 * 描  述：对象关系映射类
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace HZYT.DBUtility
{
    public class ORM
    {
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        public static bool Add(object classObject, string AssemblyName, string ConnString)
        {
            int intMaxID = 0, intFlag = 0;
            return Add(classObject, out intMaxID, intFlag, AssemblyName, ConnString);
        }

        /// <summary>
        /// 添加一个实体并且返回其ID标识
        /// </summary>
        /// <param name="classObject"></param>
        /// <param name="intMaxID"></param>
        /// <returns></returns>
        public static bool Add(object classObject, out int intMaxID, string AssemblyName, string ConnString)
        {
            intMaxID = 0;
            int intFlag = 1;
            return Add(classObject, out intMaxID, intFlag, AssemblyName, ConnString);
        }

        /// <summary>
        /// 添加实体并判断是否返回最大的编号
        /// </summary>
        /// <param name="classObject"></param>
        /// <param name="intMaxID"></param>
        /// <param name="intFlag">当intFlag=0时，则不去取intMaxID，等于1则相反</param>
        /// <returns></returns>
        private static bool Add(object classObject, out int intMaxID, int intFlag, string AssemblyName, string ConnString)
        {
            //声名输出参数
            intMaxID = 0;

            //获取表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //获取主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);
            //获取除主键以外的字段字典
            Dictionary<string, string> BaseFieldMapping = Mapping.GetBaseFieldMapping(classObject, AssemblyName);
            //获取 "属性--值" 字典
            Dictionary<string, string> FieldValueMapping = Model.GetValueMapping(classObject, BaseFieldMapping);

            //创建SQL语句
            string strSQL = SQL.CreateInsert(classObject, strTableName, IdentityMapping, BaseFieldMapping, FieldValueMapping, intFlag);

            //执行SQL
            return SQL.ExecInsert(strSQL, out intMaxID, intFlag, ConnString);
        }



        /// <summary>
        /// 保存已以存在的实体，即修改实体属性(实体必须有ID标识)
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        public static bool Save(object classObject, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //声名主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);
            //声名除主键以外的字段字典
            Dictionary<string, string> BaseFieldMapping = Mapping.GetBaseFieldMapping(classObject, AssemblyName);
            //获取 "属性--值" 字典
            Dictionary<string, string> FieldValueMapping = Model.GetValueMapping(classObject, BaseFieldMapping);

            //创建SQL语句
            string strSQL = SQL.CreateUpdate(classObject, strTableName, IdentityMapping, BaseFieldMapping, FieldValueMapping);

            //执行SQL
            return SQL.ExecUpdate(strSQL, ConnString);
        }

        /// <summary>
        /// 获取实体(实体必须有ID标识)
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        public static object Get(object classObject, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //声名主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);
            //声名除主键以外的字段字典
            Dictionary<string, string> FieldMapping = Mapping.GetAllFieldMapping(classObject, AssemblyName);

            //创建SQL语句
            string strSQL = SQL.CreateSelect(classObject, strTableName, IdentityMapping, FieldMapping);

            //执行SQL
            return SQL.ExecSelect(strSQL, classObject, FieldMapping, ConnString);
        }

        /// <summary>
        /// 获取实体(查询条件不能为空)
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        public static object Get(object classObject, string strWHERE, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //声名除主键以外的字段字典
            Dictionary<string, string> FieldMapping = Mapping.GetAllFieldMapping(classObject, AssemblyName);

            //创建SQL语句
            string strSQL = SQL.CreateSelect(classObject, strTableName, strWHERE, FieldMapping);

            //执行SQL
            return SQL.ExecSelect(strSQL, classObject, FieldMapping, ConnString);
        }

        /// <summary>
        /// 移除实体(实体必须有ID标识)
        /// </summary>
        /// <param name="classObject">传入的实体类</param>
        /// <returns></returns>
        public static bool Remove(object classObject, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //声名主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);

            //创建SQL语句
            string strSQL = SQL.CreateDelete(classObject, strTableName, IdentityMapping);

            //执行SQL
            return SQL.ExecDelete(strSQL, ConnString);
        }

        /// <summary>
        /// 批量移除实体(实体必须有ID标识)
        /// </summary>
        /// <param name="classObject">传入的实体类</param>
        /// <returns></returns>
        public static bool Remove(List<object> listClassObject, string AssemblyName, string ConnString)
        {
            object classObject = null;
            if (listClassObject.Count > 0)
            {
                classObject = listClassObject[0];
            }

            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);

            //声名主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);

            //创建SQL语句
            string strSQL = SQL.CreateDeleteList(listClassObject, strTableName, IdentityMapping);

            //执行SQL
            return SQL.ExecDelete(strSQL, ConnString);
        }

        /// <summary>
        /// 获取列表全部
        /// </summary>
        /// <param name="classObject">实体的类型</param>
        /// <returns>符合条件的实体列表</returns>
        public static List<object> GetList(object classObject, string AssemblyName, string ConnString)
        {
            string strWhere = " 1 = 1 ";
            return GetList(classObject, strWhere, AssemblyName, ConnString);
        }

        /// <summary>
        /// 根据自己定义查询条件获取列表
        /// </summary>
        /// <param name="classObject">实体的类型</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>符合条件的实体列表</returns>
        public static List<object> GetList(object classObject, string strWHERE, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);

            //声名字段字典
            Dictionary<string, string> FieldMapping = Mapping.GetAllFieldMapping(classObject, AssemblyName);

            //创建SQL语句
            string strSQL = SQL.CreateSelectList(classObject, strTableName, FieldMapping, strWHERE);

            //执行SQL
            return SQL.ExecSelectList(strSQL, classObject, FieldMapping, ConnString);
        }

        /// <summary>
        /// 分页获取列表（只含有基础查询条件）
        /// </summary>
        /// <param name="classObject">实体的类型</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentCount">当前页面</param>
        /// <param name="intTotalCount">输出总记录数</param>
        /// <returns>符合条件的实体列表</returns>
        public static List<object> GetList(object classObject, int intPageSize, int intCurrentCount, out int intTotalCount, string AssemblyName, string ConnString)
        {
            string strWhere = " 1 = 1 ";
            return GetList(classObject, intPageSize, intCurrentCount, strWhere, out intTotalCount, AssemblyName, ConnString);
        }

        /// <summary>
        /// 分页获取列表（含有基础查询条件的同时可以附加查询条件）
        /// </summary>
        /// <param name="classObject">传入的实体</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentCount">当前页面</param>
        /// <param name="strWhere">查询条件(例： ID = 1 ,不用加 1=1 或是 WHERE 。如果要排序的话直接加 ORDER BY )</param>
        /// <param name="intTotalCount">输出总记录数</param>
        /// <returns>符合条件的实体列表</returns>
        public static List<object> GetList(object classObject, int intPageSize, int intCurrentCount, string strWhere, out int intTotalCount, string AssemblyName, string ConnString)
        {
            //声名表名称
            string strTableName = Mapping.GetTableName(classObject, AssemblyName);
            //声名主键字典
            Dictionary<string, string> IdentityMapping = Mapping.GetIdentityMapping(classObject, AssemblyName);
            //声名除主键以外的字段字典
            Dictionary<string, string> FieldMapping = Mapping.GetAllFieldMapping(classObject, AssemblyName);

            //转换成为 List
            return SQL.ChangeDateSetToList(classObject, FieldMapping, SqlCommon.GetList(ConnString, SQL.GetIdentity(IdentityMapping), intPageSize, intCurrentCount, strTableName, strWhere, out intTotalCount));
        }

        /// <summary>
        /// 把DataSet转换成List
        /// </summary>
        /// <param name="classObject"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<object> ChangeDateSetToList(object classObject, System.Data.DataSet ds, string AssemblyName)
        {
            //声名除主键以外的字段字典
            Dictionary<string, string> FieldMapping = Mapping.GetAllFieldMapping(classObject, AssemblyName);
            //转换成为 List
            return SQL.ChangeDateSetToList(classObject, FieldMapping, ds);
        }
    }
}
