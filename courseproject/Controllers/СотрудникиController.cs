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
    public class СотрудникиController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Сотрудники
        public ActionResult Index()
        {
            Session["PhiltrSalary"] = "asc";
            var сотрудники = db.Сотрудники.Include(с => с.Объекты);
            return View(сотрудники.ToList());
        }

        // GET: Сотрудники/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        // GET: Сотрудники/Create
        public ActionResult Create()
        {
            //ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование");
            List<SelectListItem> items = new SelectList(db.Объекты, "ID_Объекта", "Наименование").ToList();
            items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_объекта = items;
            return View();
        }

        // POST: Сотрудники/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_сотрудника,Фамилия,Имя,Отчество,Серия_и_номер_паспорта,Пол,Дата_рождения,ID_объекта,Должность,Дата_вступления_в_должность,Зарплата,e_mail,Номер_телефона")] Сотрудники сотрудники)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Сотрудники.Add(сотрудники);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", сотрудники.ID_объекта);
                List<SelectListItem> items = new SelectList(db.Объекты, "ID_Объекта", "Наименование", сотрудники.ID_объекта).ToList();
                items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_объекта = items;
                return View(сотрудники);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Сотрудники/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", сотрудники.ID_объекта);
            List<SelectListItem> items = new SelectList(db.Объекты, "ID_Объекта", "Наименование", сотрудники.ID_объекта).ToList();
            items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_объекта = items;
            return View(сотрудники);
        }

        // POST: Сотрудники/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_сотрудника,Фамилия,Имя,Отчество,Серия_и_номер_паспорта,Пол,Дата_рождения,ID_объекта,Должность,Дата_вступления_в_должность,Зарплата,e_mail,Номер_телефона")] Сотрудники сотрудники)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(сотрудники).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", сотрудники.ID_объекта);
                List<SelectListItem> items = new SelectList(db.Объекты, "ID_Объекта", "Наименование", сотрудники.ID_объекта).ToList();
                items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_объекта = items;
                return View(сотрудники);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Сотрудники/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            if (сотрудники == null)
            {
                return HttpNotFound();
            }
            return View(сотрудники);
        }

        // POST: Сотрудники/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Сотрудники сотрудники = db.Сотрудники.Find(id);
            db.Сотрудники.Remove(сотрудники);
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

        public ActionResult ChangeSalary(int idEmpl, decimal newSalary)
        {
            try
            {
                if (newSalary < 0)
                    return RedirectToAction("Error", "Home", new { code = 3 });
                db.ChangeSalary(idEmpl, newSalary);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 3 });
            }
        }

        public ActionResult FindBySecondName (string sname)
        {
            try
            {
                if (sname != "")
                    return View("Index", db.Сотрудники.Where(o => o.Фамилия == sname).Select(a => a));
                else
                    return View("Index", db.Сотрудники.Select(a => a));
            }
            catch (Exception)
            {
                return View("Index", db.Сотрудники.Select(a => a));
            }
        }

        public ActionResult PhiltrSalary()
        {
            if (Session["PhiltrSalary"].ToString() == "asc")
            {
                Session["PhiltrSalary"] = "desc";
                return View("Index", db.Сотрудники.OrderBy(a => a.Зарплата).ThenBy(o => o.Дата_вступления_в_должность));
            }
            else
            {
                Session["PhiltrSalary"] = "asc";
                return View("Index", db.Сотрудники.OrderByDescending(a => a.Зарплата).ThenBy(o => o.Дата_вступления_в_должность));
            }
        }
    }
}
