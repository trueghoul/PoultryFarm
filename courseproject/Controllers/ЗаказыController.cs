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
    public class ЗаказыController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Заказы
        public ActionResult Index()
        {
            Session["PhiltrFinalPrice"] = "asc";
            var заказы = db.Заказы.Include(з => з.Клиенты).Include(з => з.Объекты).Include(з => з.Продукция);
            return View(заказы.ToList());
        }

        // GET: Заказы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = db.Заказы.Find(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            return View(заказы);
        }

        // GET: Заказы/Create
        public ActionResult Create()
        {
            //ViewBag.Наименование_Юридического_Лица = new SelectList(db.Клиенты, "Наименование_Юридического_Лица", "Наименование_Юридического_Лица");
            List<SelectListItem> items1 = new SelectList(db.Клиенты, "Наименование_Юридического_Лица", "Наименование_Юридического_Лица").ToList();
            items1.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.Наименование_Юридического_Лица = items1;

            //ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование");
            List<SelectListItem> items2 = new SelectList(db.Объекты, "ID_Объекта", "Наименование").ToList();
            items2.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_объекта = items2;

            //ViewBag.ID_продукции = new SelectList(db.Продукция, "ID_продукции", "ID_продукции");
            List<SelectListItem> items3 = new SelectList(db.Продукция, "ID_продукции", "ID_продукции").ToList();
            items3.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_продукции = items3;

            return View();
        }

        // POST: Заказы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_заказа,Собственный,ID_продукции,Наименование_товара,ID_объекта,Количество,Итоговая_цена,Дата_заказа,Наименование_Юридического_Лица")] Заказы заказы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Заказы.Add(заказы);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                List<SelectListItem> items1 = new SelectList(db.Клиенты, "Наименование_Юридического_Лица", "Наименование_Юридического_Лица").ToList();
                items1.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.Наименование_Юридического_Лица = items1;

                List<SelectListItem> items2 = new SelectList(db.Объекты, "ID_Объекта", "Наименование").ToList();
                items2.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_объекта = items2;

                List<SelectListItem> items3 = new SelectList(db.Продукция, "ID_продукции", "ID_продукции").ToList();
                items3.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_продукции = items3;

                return View(заказы);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Заказы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = db.Заказы.Find(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> items1 = new SelectList(db.Клиенты, "Наименование_Юридического_Лица", "Наименование_Юридического_Лица").ToList();
            items1.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.Наименование_Юридического_Лица = items1;

            List<SelectListItem> items2 = new SelectList(db.Объекты, "ID_Объекта", "Наименование").ToList();
            items2.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_объекта = items2;

            List<SelectListItem> items3 = new SelectList(db.Продукция, "ID_продукции", "ID_продукции").ToList();
            items3.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_продукции = items3;
            return View(заказы);
        }

        // POST: Заказы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_заказа,Собственный,ID_продукции,Наименование_товара,ID_объекта,Количество,Итоговая_цена,Дата_заказа,Наименование_Юридического_Лица")] Заказы заказы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(заказы).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> items1 = new SelectList(db.Клиенты, "Наименование_Юридического_Лица", "Наименование_Юридического_Лица").ToList();
                items1.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.Наименование_Юридического_Лица = items1;

                List<SelectListItem> items2 = new SelectList(db.Объекты, "ID_Объекта", "Наименование").ToList();
                items2.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_объекта = items2;

                List<SelectListItem> items3 = new SelectList(db.Продукция, "ID_продукции", "ID_продукции").ToList();
                items3.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_продукции = items3;
                return View(заказы);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Заказы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = db.Заказы.Find(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            return View(заказы);
        }

        // POST: Заказы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Заказы заказы = db.Заказы.Find(id);
            db.Заказы.Remove(заказы);
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

        public ActionResult PhiltrFinalPrice()
        {
            if (Session["PhiltrFinalPrice"].ToString() == "asc")
            {
                Session["PhiltrFinalPrice"] = "desc";
                return View("Index", db.Заказы.OrderBy(a => a.Итоговая_цена).ThenBy(o => o.Дата_заказа));
            }
            else
            {
                Session["PhiltrFinalPrice"] = "asc";
                return View("Index", db.Заказы.OrderByDescending(a => a.Итоговая_цена).ThenBy(o => o.Дата_заказа));
            }
        }

        public ActionResult NewOrder(int idProd, int Number, string name)
        {
            try
            {
                db.NewOrder(idProd, Number, name);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 6 });
            }
        }

        public ActionResult FindByName(string name)
        {
            try
            {
                if (name != "")
                    return View("Index", db.Заказы.Where(o => o.Наименование_Юридического_Лица == name).Select(a => a));
                else
                    return View("Index", db.Заказы.Select(a => a));
            }
            catch (Exception)
            {
                return View("Index", db.Заказы.Select(a => a));
            }
        }
    }
}
