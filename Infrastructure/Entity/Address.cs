using EmployeeManagement.Infrastructure.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.Infrastructure.Entity
{
    public class Address : Tracker, IEntity
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
