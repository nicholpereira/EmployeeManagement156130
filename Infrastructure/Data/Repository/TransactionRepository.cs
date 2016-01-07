using EmployeeManagement.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data.Repsitory
{
    public class TransactionRepository : AbstractRepository<Transaction>
    {
        public TransactionRepository()
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

        public List<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Transaction Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> Find(Func<Transaction, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Add(Transaction obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Transaction Update(Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}