using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EmployeeManagement.Infrastructure.Helpers
{
    public static class SqlHelper
    {
        public static SqlParameter[] GetInsertParameters<T>(T obj, params string[] columnsToInclude) where T : class
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            columnsToInclude = columnsToInclude.Select(x => x.ToLower()).ToArray();

            foreach (PropertyInfo p in properties)
            {
                var lowercase = p.Name.ToLower();

                //skip if column doesnt belong to the include list or if it is primary col
                if (!columnsToInclude.Contains(lowercase) || lowercase == "id") continue;

                parameters.Add(new SqlParameter(p.Name, p.GetValue(obj)));
            }

            return parameters.ToArray();
        }

        public static SqlParameter[] GetUpdateParameters<T>(T obj, List<string> exlude = null) where T : class
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in properties)
            {
                if (exlude != null && exlude.Contains(p.Name.ToLower())) continue;
                parameters.Add(new SqlParameter(p.Name, p.GetValue(obj)));
            }

            return parameters.ToArray();
        }
        public static SqlParameter[] GetUpdateParameters<T>(T obj, params string[] columnsToInclude) where T : class
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            columnsToInclude = columnsToInclude.Select(x => x.ToLower()).ToArray();

            foreach (PropertyInfo p in properties)
            {
                var lowercase = p.Name.ToLower();

                //skip if column doesnt belong to the include list
                if (!columnsToInclude.Contains(lowercase) && lowercase != "id") continue;

                parameters.Add(new SqlParameter(p.Name, p.GetValue(obj)));
            }

            return parameters.ToArray();
        }

    }
}