using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace HZYT.DBUtility
{
    public class Mapping
    {
        /// <summary>
        /// 获取 Mapping 中的表名称
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        internal static string GetTableName(object classObject, string AssemblyName)
        {
            //获取 Mapping 类型
            Type mappingType = null;

            //获取 Mapping 映射类
            object MappingClass = GetMappingObject(classObject, ref mappingType, AssemblyName);

            //根据 Mapping 中的 GetTableName 方法获取表名称
            return (string)mappingType.InvokeMember("GetTableName", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, MappingClass, new object[] { });
        }


        /// <summary>
        /// 获取 Mapping 中的主键映射
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        internal static Dictionary<string, string> GetIdentityMapping(object classObject, string AssemblyName)
        {
            //获取 Mapping 类型
            Type mappingType = null;

            //获取 Mapping 映射类
            object MappingClass = GetMappingObject(classObject, ref mappingType, AssemblyName);

            //根据 Mapping 中的 GetIdentityMapping 方法获取主键名称
            return (Dictionary<string, string>)mappingType.InvokeMember("GetIdentityMapping", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, MappingClass, new object[] { });
        }

        /// <summary>
        /// 获取 Mapping 中的其他字段映射
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        internal static Dictionary<string, string> GetBaseFieldMapping(object classObject, string AssemblyName)
        {
            //获取 Mapping 类型
            Type mappingType = null;

            //获取 Mapping 映射类
            object MappingClass = GetMappingObject(classObject, ref mappingType, AssemblyName);

            //根据 Mapping 中的 GetIdentityMapping 方法获取主键名称
            return (Dictionary<string, string>)mappingType.InvokeMember("GetBaseFieldMapping", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, MappingClass, new object[] { });
        }

        /// <summary>
        /// 获取属性列表（整合主键）
        /// </summary>
        /// <param name="IdentityMapping"></param>
        /// <param name="BaseFieldMapping"></param>
        /// <returns></returns>
        internal static Dictionary<string, string> GetAllFieldMapping(object classObject, string AssemblyName)
        {
            //获取主键映射
            Dictionary<string, string> IdentityMapping = GetIdentityMapping(classObject, AssemblyName);

            //获取其他字段映射
            Dictionary<string, string> BaseFieldMapping = GetBaseFieldMapping(classObject, AssemblyName);

            //克隆 BaseFieldMapping  
            Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
            foreach (string strKey in BaseFieldMapping.Keys)
            {
                tempDictionary.Add(strKey, BaseFieldMapping[strKey]);
            }
            //清空
            BaseFieldMapping.Clear();
            //添加主键
            foreach (string strKey in IdentityMapping.Keys)
            {
                //取出数据库主键的名称
                BaseFieldMapping.Add(strKey, IdentityMapping[strKey]);
            }
            //添加其他属性
            foreach (string strKey in tempDictionary.Keys)
            {
                BaseFieldMapping.Add(strKey, tempDictionary[strKey]);
            }
            return BaseFieldMapping;
        }

        /// <summary>
        /// 获取 Mapping 映射类 及 映射类的类型
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        internal static object GetMappingObject(object classObject, ref Type mappingType, string AssemblyName)
        {
            //获取传入的实体的类型
            Type type = classObject.GetType();

            //传入的类名称
            string strClassName = type.ToString();

            //如果获取到的是带有命名空间的类型
            if (type.ToString().Contains("."))
            {
                strClassName = type.ToString().Substring(type.ToString().LastIndexOf("."), type.ToString().Length - type.ToString().LastIndexOf("."));
            }

            //取出映射文件名称  ClassName + Mapping
            string strMappingClassName = AssemblyName + strClassName + "Mapping";

            //反射出映射类的实例
            object MappingObject = Assembly.Load(AssemblyName).CreateInstance(strMappingClassName);

            //获取 Mapping 的类型
            mappingType = MappingObject.GetType();

            //根据 Mapping 类型创建对象
            object MappingClass = mappingType.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

            //返回对象的实例
            return MappingClass;
        }

    }
}
