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
    public class AddressRepository : AbstractRepository<Address>
    {
        public AddressRepository()
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
        public List<Address> GetAll()
        {
            string query = "select * from Addresses";
            var data = SqlHandler.SelectQuery(GetConnection(), query);
            return data.ToList<Address>();
        }

        public Address Get(int id)
        {
            string query = "select * from Addresses where id = @id";
            SqlParameter param = new SqlParameter("id", id);
            var data = SqlHandler.SelectQuery(GetConnection(), query, new SqlParameter[] { param });
            var first = data.ToList<Address>().FirstOrDefault();
            return first;
        }

        public List<Address> Find(Func<Address, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public bool Add(Address obj)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("Street", obj.Street));
            paramList.Add(new SqlParameter("City", obj.City));
            paramList.Add(new SqlParameter("LandMark", obj.LandMark));
            paramList.Add(new SqlParameter("Country", obj.Country));
            paramList.Add(new SqlParameter("PinCode", obj.PinCode));
            paramList.Add(new SqlParameter("Description", obj.Description));
            paramList.Add(new SqlParameter("IsActive", obj.IsActive));

            return SqlHandler.InsertQuery(GetConnection(), "Addresses", paramList.ToArray());
        }

        public bool Delete(int id)
        {
            string query = "delete from Addresses where id =?";
            SqlParameter param = new SqlParameter("id", id);
            return SqlHandler.UpdateQuery(GetConnection(), query, new SqlParameter[] { param });
        }

        public Address Update(Address obj)
        {
            var param = new SqlParameter[12];
            param[0] = new SqlParameter("id", obj.Id);
            param[1] = new SqlParameter("city", obj.City);
            param[2] = new SqlParameter("country", obj.Country);
            param[3] = new SqlParameter("pincode", obj.PinCode);
            param[4] = new SqlParameter("description", obj.Description);
            param[5] = new SqlParameter("landmark", obj.LandMark);
            param[6] = new SqlParameter("street", obj.Street);

            var result = SqlHandler.UpdateQuery(GetConnection(), "Addresses", param);

            return obj;
        }
    }
}