/*
 * 
 * �����ˣ����ַ�
 * 
 * ʱ  �䣺2009-08-28
 * 
 * ��  ����ͨ�����ݲ����࣬���ڰ����������ݲ�������
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
        /// ͨ�÷�ҳ�洢���̣���������ѯ���������ֶΣ����������ֶεĽ�������
        /// </summary>
        /// <param name="intPageSize">ÿҳ��¼��</param>
        /// <param name="intCurrentCount">��ǰ��¼������ҳ��*ÿҳ��¼����</param>
        /// <param name="strTableName">������</param>
        /// <param name="strWhere">��ѯ����������"ID>1000 AND Name like '%LiLinFeng%'" ����������ֱ���ں���ӣ�����" ORDER BY ID DESC,NAME ASC"</param>
        /// <param name="intTotalCount">��¼����</param>
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
