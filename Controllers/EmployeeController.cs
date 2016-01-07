using EmployeeManagement.Infrastructure.Data.Repsitory;
using EmployeeManagement.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var employees = DataProvider.Instance.Employees.GetAll();
            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee obj)
        {
            var success = DataProvider.Instance.Addresses.Add(obj.Address);
            if (success)
            {
                var address = DataProvider.Instance.Addresses.GetAll().LastOrDefault();
                obj.AddressId = address.Id;
                obj.EmpCode = GenerateRandomString(5);
                var result = DataProvider.Instance.Employees.Add(obj);
            }
            return RedirectToAction("Index");
        }

        private string GenerateRandomString(int len = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, len)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        public ActionResult Edit(int id)
        {
            var entity = DataProvider.Instance.Employees.Get(id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            var entity = DataProvider.Instance.Employees.Update(employee);
            return View();
        }

        public ActionResult Details(int id)
        {
            var entity = DataProvider.Instance.Employees.Get(id);
            return View(entity);
        }

        public ActionResult Delete(int id)
        {
            DataProvider.Instance.Employees.Delete(id);
            return RedirectToAction("Index");
        }
    }
}