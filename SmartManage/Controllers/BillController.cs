using Microsoft.AspNetCore.Mvc;
using SmartManage.Data;
using SmartManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManage.Controllers
{
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BillController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Bill> objList = _db.Bills;
            return View(objList);
        }

        public IActionResult AddBill(int? id)
        {
            return View();
        }

        //POST-add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBill(Bill obj)
        {
            if (ModelState.IsValid)
            {
                _db.Bills.Add(obj);
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View(obj);
        }

        //GET-delete
        public IActionResult DeleteBill(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Bills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBillPost(int? id)
        {
            var obj = _db.Bills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Bills.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
