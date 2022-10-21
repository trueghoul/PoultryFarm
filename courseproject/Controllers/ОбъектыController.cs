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
    public class ОбъектыController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Объекты
        public ActionResult Index()
        {
            var объекты = db.Объекты.Include(о => о.Склады).Include(о => о.Цеха_производства);
            return View(объекты.ToList());
        }

        // GET: Объекты/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты объекты = db.Объекты.Find(id);
            if (объекты == null)
            {
                return HttpNotFound();
            }
            return View(объекты);
        }

        // GET: Объекты/Create
        public ActionResult Create()
        {
            ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта");
            ViewBag.ID_объекта = new SelectList(db.Цеха_производства, "ID_объекта", "ID_объекта");
            return View();
        }

        // POST: Объекты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_объекта,Наименование,Площадь,Влажность,Наличие_морозильных_камер,Температура")] Объекты объекты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Объекты.Add(объекты);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", объекты.ID_объекта);
                ViewBag.ID_объекта = new SelectList(db.Цеха_производства, "ID_объекта", "ID_объекта", объекты.ID_объекта);
                return View(объекты);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Объекты/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты объекты = db.Объекты.Find(id);
            if (объекты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", объекты.ID_объекта);
            ViewBag.ID_объекта = new SelectList(db.Цеха_производства, "ID_объекта", "ID_объекта", объекты.ID_объекта);
            return View(объекты);
        }

        // POST: Объекты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_объекта,Наименование,Площадь,Влажность,Наличие_морозильных_камер,Температура")] Объекты объекты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(объекты).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", объекты.ID_объекта);
                ViewBag.ID_объекта = new SelectList(db.Цеха_производства, "ID_объекта", "ID_объекта", объекты.ID_объекта);
                return View(объекты);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Объекты/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объекты объекты = db.Объекты.Find(id);
            if (объекты == null)
            {
                return HttpNotFound();
            }
            return View(объекты);
        }

        // POST: Объекты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Объекты объекты = db.Объекты.Find(id);
            db.Объекты.Remove(объекты);
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
