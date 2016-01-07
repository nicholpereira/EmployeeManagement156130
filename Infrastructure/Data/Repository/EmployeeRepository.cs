using EmployeeManagement.Infrastructure.Entity;
using EmployeeManagement.Infrastructure.Extensions;
using EmployeeManagement.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace EmployeeManagement.Infrastructure.Data
{
    public class EmployeeRepository : AbstractRepository<Employee>
    {
        public EmployeeRepository()
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

        public List<Employee> GetAll()
        {
            string query = "select * from Employees inner join Addresses on Addresses.Id = Employees.AddressId";
            var data = SqlHandler.SelectQuery(GetConnection(), query);
            var employees = data.ToList<Employee>();
            var addresses = data.ToList<Address>();
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].Address = addresses[i];
            }

            return employees;
            //return data.ToList<Employee>();
        }

        public Employee Get(int id)
        {
            string query = "select * from Employees inner join Addresses on Addresses.Id = Employees.AddressId where Employees.id = @id";
            SqlParameter param = new SqlParameter("id", id);
            var rows = SqlHandler.SelectQuery(GetConnection(), query, new SqlParameter[] { param });
            var employee = rows.ToList<Employee>().FirstOrDefault();
            var address = rows.ToList<Address>().FirstOrDefault();
            var company = rows.ToList<Company>().FirstOrDefault();

            employee.Address = address;
            employee.Company = company;

            return employee;
        }

        public List<Employee> Find(Func<Employee, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public bool Add(Employee obj)
        {
            //hardcoded
            //obj.DOB = new DateTime(1980, 8, 15);

            var parameters = SqlHelper.GetInsertParameters<Employee>(obj, "Name", "AddressId", "EmpCode", "Designation", "DOJ", "DOB", "CompanyId");
            return SqlHandler.InsertQuery(GetConnection(), "Employees", parameters);
        }

        public bool Delete(int id)
        {
            SqlParameter param = new SqlParameter("id", id);
            return SqlHandler.DeleteQuery(GetConnection(), "Employees", "id", param);
        }

        public Employee Update(Employee obj)
        {
            var parameters = SqlHelper.GetUpdateParameters<Employee>(obj, "name", "designation");
            var result = SqlHandler.UpdateQuery(GetConnection(), "Employees", parameters);

            return obj;
        }
    }
}