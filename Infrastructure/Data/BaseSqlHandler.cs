using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data
{
    public class BaseSqlHandler
    {
        private void MakeSureConnectionIsOpen(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Broken || cn.State == ConnectionState.Closed)
                cn.Open();
        }
        public DataTable SelectQuery(SqlConnection cn, string query, SqlParameter[] sqlParams = null)
        {
            try
            {

                MakeSureConnectionIsOpen(cn);

                SqlCommand cmd = new SqlCommand(query, cn);

                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool InsertQuery(SqlConnection cn, string tableName, SqlParameter[] sqlParams)
        {
            try
            {
                string query = "Insert into {0} ({1}) values({2})";
                string columns = string.Empty;
                string values = string.Empty;

                var availableFields = sqlParams.Where(x => x.Value != null).ToList();
                columns = String.Join(",", availableFields.Select(x => x.ParameterName));
                values = String.Join(",", availableFields.Select(x => String.Concat("@", x.ParameterName)));

                query = String.Format(query, tableName, columns, values);

                MakeSureConnectionIsOpen(cn);
                SqlCommand cmd = new SqlCommand(query, cn);

                if (availableFields != null)
                {
                    var idp = availableFields.Where(x => x.ParameterName.ToLower() == "id").FirstOrDefault();

                    if (idp != null)
                        availableFields.Remove(idp);

                    cmd.Parameters.AddRange(availableFields.ToArray());
                }
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateQuery(SqlConnection cn, string tablename, SqlParameter[] sqlParams = null, string primaryColumnName = "Id")
        {
            try
            {

                string query = "Update {0} set {1} where {2}";

                var availableFields = sqlParams.Where(x => x.Value != null).ToList();
                List<string> cols = new List<string>();
                string whereClause = string.Empty;
                foreach (var item in availableFields)
                {
                    var s = String.Concat(item.ParameterName, "=", "@" + item.ParameterName);

                    if (item.ParameterName.ToLower() == primaryColumnName.ToLower())
                    {
                        whereClause = s;
                        continue;
                    }

                    cols.Add(s);
                }

                string columns = String.Join(",", cols);

                query = String.Format(query, tablename, columns, whereClause);

                MakeSureConnectionIsOpen(cn);

                SqlCommand cmd = new SqlCommand(query, cn);

                if (sqlParams != null)
                    cmd.Parameters.AddRange(availableFields.ToArray());

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteQuery(SqlConnection cn, string tablename, string primaryColumnName = "Id", params SqlParameter[] parameters)
        {
            try
            {
                string query = "Delete From {0} where {1}";

                var availableFields = parameters.Where(x => x.Value != null).ToList();
                string whereClause = string.Empty;
                foreach (var item in availableFields)
                {
                    if (item.ParameterName.ToLower() == primaryColumnName.ToLower())
                    {
                        whereClause = String.Concat(item.ParameterName, "=", "@" + item.ParameterName);
                        break;
                    }
                }

                query = String.Format(query, tablename, whereClause);

                MakeSureConnectionIsOpen(cn);

                SqlCommand cmd = new SqlCommand(query, cn);

                if (parameters != null)
                    cmd.Parameters.AddRange(availableFields.ToArray());

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //public bool UpdateQuery(SqlConnection cn, string query, SqlParameter[] sqlParams = null)
        //{
        //    try
        //    {
        //        MakeSureConnectionIsOpen(cn);

        //        SqlCommand cmd = new SqlCommand(query, cn);

        //        if (sqlParams != null)
        //            cmd.Parameters.AddRange(sqlParams);

        //        cmd.ExecuteNonQuery();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}