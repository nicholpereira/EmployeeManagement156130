using EmployeeManagement.Infrastructure.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Entity
{
    public class Account : Tracker, IEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Employee Employee { get; set; }
    }
}