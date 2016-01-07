using EmployeeManagement.Infrastructure.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Infrastructure.Entity
{
    public class Employee : Tracker, IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be left empty")]
        public string Name { get; set; }
        
        public int AddressId { get; set; }

        [Required(ErrorMessage = "EmpCode cannot be left empty")]
        public string EmpCode { get; set; }

        [Required(ErrorMessage = "Designation cannot be left empty")]
        public string Designation { get; set; }

        public DateTime DOJ { get; set; }

        public DateTime DOB { get; set; }
        
        public int CompanyId { get; set; }
        
        public Company Company { get; set; }
        
        public Address Address { get; set; }
    }
}