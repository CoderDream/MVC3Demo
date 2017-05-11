/*
 * 
 * 创建人：李林峰
 * 
 * 时  间：2009-08-28
 * 
 * 描  述：通用数据操作类，用于帮助公用数据操作部分
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HZYT.DBUtility
{
    public sealed class SqlCommon
    {
        /// <summary>
        /// 通用分页存储过程，有条件查询，有排序字段，按照排序字段的降序排列
        /// </summary>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentCount">当前记录数量（页码*每页记录数）</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="strWhere">查询条件，例："ID>1000 AND Name like '%LiLinFeng%'" 排序条件，直接在后面加，例：" ORDER BY ID DESC,NAME ASC"</param>
        /// <param name="intTotalCount">记录总数</param>
        /// <returns></returns>
        public static DataSet GetList(string connectionString, string strIdentityName, int intPageSize, int intCurrentCount, string strTableName, string strWhere, out int intTotalCount)
        {
            SqlParameter[] parmList =
                {
                    new SqlParameter("@varIdentityName",strIdentityName),
                    new SqlParameter("@intPageSize",intPageSize),
                    new SqlParameter("@intCurrentCount",intCurrentCount),
                    new SqlParameter("@varTableName",strTableName),
                    new SqlParameter("@varWhere",strWhere),
                    new SqlParameter("@intTotalCount",SqlDbType.Int,4)
                };
            parmList[5].Direction = ParameterDirection.Output;
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "prPager", parmList);
            intTotalCount = Convert.ToInt32(parmList[5].Value);
            return ds;
        }
    }
}
