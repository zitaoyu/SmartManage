using Microsoft.AspNetCore.Mvc;
using SmartManage.Data;
using SmartManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManage.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> objList = _db.Customers;
            return View(objList);
        }

        //GET-add
        public IActionResult AddCustomer()
        {
            return View();
        }

        //POST-add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(obj);
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View(obj);
        }

        //GET-edit
        public IActionResult EditCustomerInfo(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomerInfo(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-delete
        public IActionResult DeleteCustomerInfo(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomerInfoPost(int? id)
        {
            var obj = _db.Customers.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
