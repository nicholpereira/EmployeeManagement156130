using EmployeeManagement.Infrastructure.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Infrastructure.Entity
{
    public class Company : Tracker, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
