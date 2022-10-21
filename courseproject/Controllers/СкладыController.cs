using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using courseproject.Models;

namespace courseproject.Controllers
{
    public class СкладыController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Склады
        public ActionResult Index()
        {
            var склады = db.Склады.Include(с => с.Объекты);
            return View(склады.ToList());
        }

        // GET: Склады/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Склады склады = db.Склады.Find(id);
            if (склады == null)
            {
                return HttpNotFound();
            }
            return View(склады);
        }

        // GET: Склады/Create
        public ActionResult Create()
        {
            ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование");
            return View();
        }

        // POST: Склады/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_объекта,Общая_вместимость,Занято,Свободно,Свободно_")] Склады склады)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (склады.Свободно_ >= 0 && склады.Свободно_ <= 100)
                    {
                        db.Склады.Add(склады);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("Свободно_", "Недопустимый процент свободного помещения склада.");
                }

                ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", склады.ID_объекта);
                return View(склады);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Склады/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Склады склады = db.Склады.Find(id);
            if (склады == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", склады.ID_объекта);
            return View(склады);
        }

        // POST: Склады/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_объекта,Общая_вместимость,Занято,Свободно,Свободно_")] Склады склады)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (склады.Свободно_ >= 0 && склады.Свободно_ <= 100)
                    {
                        db.Entry(склады).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("Свободно_", "Недопустимый процент свободного помещения склада.");
                }
                ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", склады.ID_объекта);
                return View(склады);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Склады/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Склады склады = db.Склады.Find(id);
            if (склады == null)
            {
                return HttpNotFound();
            }
            return View(склады);
        }

        // POST: Склады/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Склады склады = db.Склады.Find(id);
            db.Склады.Remove(склады);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
