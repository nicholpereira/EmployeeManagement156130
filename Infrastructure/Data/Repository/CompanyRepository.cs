using EmployeeManagement.Infrastructure.Entity;
using EmployeeManagement.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data.Repsitory
{
    public class CompanyRepository : AbstractRepository<Company>
    {
        public CompanyRepository()
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

        public List<Company> GetAll()
        {
            string query = "select * from Companies inner join Addresses on Addresses.Id = Companies.AddressId";
            var data = SqlHandler.SelectQuery(GetConnection(), query);
            var companies = data.ToList<Company>();
            var addresses = data.ToList<Address>();

            for (int i = 0; i < companies.Count; i++)
            {
                companies[i].Address = addresses[i];
            }

            return companies;
        }

        public Company Get(int id)
        {
            string query = "select * from Companies inner join Addresses on Addresses.Id = Companies.AddressId where Companies.id = @id";
            SqlParameter param = new SqlParameter("id", id);
            var rows = SqlHandler.SelectQuery(GetConnection(), query, new SqlParameter[] { param });
            
            var company = rows.ToList<Company>().FirstOrDefault();
            var address = rows.ToList<Address>().FirstOrDefault();
            company.Address = address;

            return company;
        }

        public List<Company> Find(Func<Company, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public bool Add(Company obj)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("Name", obj.Name));
            paramList.Add(new SqlParameter("AddressId", obj.AddressId));
            return SqlHandler.InsertQuery(GetConnection(), "Companies", paramList.ToArray());
        }

        public bool Delete(int id)
        {
            string query = "delete from Companies where id = @id";
            SqlParameter param = new SqlParameter("id", id);
            return SqlHandler.UpdateQuery(GetConnection(), query, new SqlParameter[] { param });
        }


        public Company Update(Company obj)
        {
            string query = "Update companies set name='@name' where id = '@id'";
            SqlParameter param1 = new SqlParameter("id", obj.Id);
            SqlParameter param2 = new SqlParameter("name", obj.Name);
            var result = SqlHandler.UpdateQuery(GetConnection(), query, new SqlParameter[] { param1, param2 });

            return obj;
        }
    }
}