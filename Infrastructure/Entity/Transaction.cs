using EmployeeManagement.Infrastructure.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Entity
{
    public class Transaction : Tracker, IEntity
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public double WithdrawlAmount { get; set; }
    }
}