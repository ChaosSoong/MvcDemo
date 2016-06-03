using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Models;
using System.Data.Entity;
using System.Data;

namespace MvcDemo.Controllers
{
    public class CodeController : Controller
    {
        //
        // GET: /Code/
        private htgisEntities1 db = new htgisEntities1();

        public ActionResult Index()
        {
            ViewBag.id = "d";
            return View(db.Code.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Code st)
        {
            db.Code.Add(st);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Code stt = db.Code.Find(Id);
            db.Code.Remove(stt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id = 0)
        {

            Code st = db.Code.Find(id);
            if (st == null)
            {
                return HttpNotFound();
            }

            return View(st);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Code st)
        {
            if (ModelState.IsValid)
            {


                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(st);
        }
    }
}
