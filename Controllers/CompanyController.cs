using EmployeeManagement.Infrastructure.Data.Repsitory;
using EmployeeManagement.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            List<Company> Companies = DataProvider.Instance.Companies.GetAll();

            return View(Companies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Company obj)
        {
            var success = DataProvider.Instance.Addresses.Add(obj.Address);
            if (success)
            {
                var address = DataProvider.Instance.Addresses.GetAll().LastOrDefault();
                obj.AddressId = address.Id;
                var result = DataProvider.Instance.Companies.Add(obj);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var company = DataProvider.Instance.Companies.Get(id);
            return View(company);
        }

        public ActionResult Details(int id)
        {
            var company = DataProvider.Instance.Companies.Get(id);
            return View(company);
        }

        public ActionResult Delete(int id)
        {
            DataProvider.Instance.Companies.Delete(id);
            return RedirectToAction("Index");
        }

    }
}