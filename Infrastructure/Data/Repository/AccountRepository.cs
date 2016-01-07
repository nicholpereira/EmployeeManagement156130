using EmployeeManagement.Infrastructure.Entity;
using EmployeeManagement.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data
{
    public class AccountRepsitory : AbstractRepository<Account>
    {
        public AccountRepsitory()
        {
            SqlHandler = new BaseSqlHandler();
        }

        private string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["EmployeeSQLConnection"];
            }
        }
        private BaseSqlHandler SqlHandler { get; set; }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public List<Account> GetAll()
        {
            string query = "select * from Accounts";
            var data = SqlHandler.SelectQuery(GetConnection(), query);
            return data.ToList<Account>();
        }

        public Account Get(int id)
        {
            string query = "select * from Accounts where id = @id";
            SqlParameter param = new SqlParameter("id", id);
            var data = SqlHandler.SelectQuery(GetConnection(), query, new SqlParameter[] { param });
            var first = data.ToList<Account>().FirstOrDefault();
            return first;
        }

        public List<Account> Find(Func<Account, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public bool Add(Account obj)
        {
            string query = "insert into Accounts('EmployeeId', 'Amount') values(EmployeeId, @Amount)";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("EmployeeId", obj.EmployeeId));
            paramList.Add(new SqlParameter("Amount", obj.Amount));

            return SqlHandler.UpdateQuery(GetConnection(), query, paramList.ToArray());
        }

        public bool Delete(int id)
        {
            string query = "delete from Accounts where id =?";
            SqlParameter param = new SqlParameter("id", id);
            return SqlHandler.UpdateQuery(GetConnection(), query, new SqlParameter[] { param });
        }


        public Account Update(Account obj)
        {
            SqlParameter param1 = new SqlParameter("id", obj.Id);
            SqlParameter param2 = new SqlParameter("amount", obj.Amount);
            var result = SqlHandler.UpdateQuery(GetConnection(), "Accounts", new SqlParameter[] { param1, param2});

            return obj;
        }
    }
}