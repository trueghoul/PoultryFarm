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
    public class ПродукцияController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Продукция
        public ActionResult Index()
        {
            Session["PhiltrNumber"] = "asc";
            var продукция = db.Продукция.Include(п => п.Склады);
            return View(продукция.ToList());
        }

        public ActionResult ClientIndex()
        {
            var продукция = db.Продукция.Include(п => п.Склады);
            var finalProd = продукция.Where(o => o.Категория != null).Select(a => a);
            return View(finalProd.ToList());
        }

        // GET: Продукция/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Продукция продукция = db.Продукция.Find(id);
            if (продукция == null)
            {
                return HttpNotFound();
            }
            return View(продукция);
        }

        // GET: Продукция/Create
        public ActionResult Create()
        {
            ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта");
            return View();
        }

        // POST: Продукция/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_продукции,Тип_продукции,Категория,Сорт,Состав,Количество,ID_объекта,Цена_за_единицу_товара")] Продукция продукция)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (продукция.Количество >= 0)
                    {
                        db.Продукция.Add(продукция);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("Количество", "Количество не может быть отрицательным.");
                }

                ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", продукция.ID_объекта);
                return View(продукция);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Продукция/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Продукция продукция = db.Продукция.Find(id);
            if (продукция == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", продукция.ID_объекта);
            return View(продукция);
        }

        // POST: Продукция/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_продукции,Тип_продукции,Категория,Сорт,Состав,Количество,ID_объекта,Цена_за_единицу_товара")] Продукция продукция)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (продукция.Количество >= 0)
                    {
                        db.Entry(продукция).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("Количество", "Количество не может быть отрицательным.");
                }
                ViewBag.ID_объекта = new SelectList(db.Склады, "ID_объекта", "ID_объекта", продукция.ID_объекта);
                return View(продукция);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Продукция/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Продукция продукция = db.Продукция.Find(id);
            if (продукция == null)
            {
                return HttpNotFound();
            }
            return View(продукция);
        }

        // POST: Продукция/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Продукция продукция = db.Продукция.Find(id);
            db.Продукция.Remove(продукция);
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

        public ActionResult ChangeProduct(int idProd, int Number)
        {
            try
            {
                db.ChangeProduct(idProd, Number);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 4 });
            }
        }

        public ActionResult PhiltrNumber()
        {
            if (Session["PhiltrNumber"].ToString() == "asc")
            {
                Session["PhiltrNumber"] = "desc";
                return View("Index", db.Продукция.OrderBy(a => a.Количество).ThenBy(o => o.Цена_за_единицу_товара));
            }
            else
            {
                Session["PhiltrNumber"] = "asc";
                return View("Index", db.Продукция.OrderByDescending(a => a.Количество).ThenBy(o => o.Цена_за_единицу_товара));
            }
        }
    }
}
