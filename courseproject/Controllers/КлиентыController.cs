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
    public class КлиентыController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Клиенты
        public ActionResult Index()
        {
            Session["PhiltrNumberOfOrders"] = "asc";
            return View(db.Клиенты.ToList());
        }

        public ActionResult ClientIndex()
        {
            string name = Session["name"].ToString();
            return View(db.Клиенты.Where(o => o.Наименование_Юридического_Лица == name).Select(a => a).ToList());
        }

        // GET: Клиенты/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // GET: Клиенты/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Клиенты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Наименование_Юридического_Лица,ИНН,ОГРН,Адрес,e_mail,Номер_телефона,Начало_сотрудничества,Количество_заказов")] Клиенты клиенты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Клиенты.Add(клиенты);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(клиенты);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Клиенты/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // POST: Клиенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Наименование_Юридического_Лица,ИНН,ОГРН,Адрес,e_mail,Номер_телефона,Начало_сотрудничества,Количество_заказов")] Клиенты клиенты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(клиенты).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(клиенты);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        public ActionResult ClientEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // POST: Клиенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientEdit([Bind(Include = "Наименование_Юридического_Лица,ИНН,ОГРН,Адрес,e_mail,Номер_телефона,Начало_сотрудничества,Количество_заказов")] Клиенты клиенты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(клиенты).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ClientIndex");
                }
                return View(клиенты);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Клиенты/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиенты клиенты = db.Клиенты.Find(id);
            if (клиенты == null)
            {
                return HttpNotFound();
            }
            return View(клиенты);
        }

        // POST: Клиенты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Клиенты клиенты = db.Клиенты.Find(id);
            db.Клиенты.Remove(клиенты);
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

        public ActionResult PhiltrNumberOfOrders()
        {
            if (Session["PhiltrNumberOfOrders"].ToString() == "asc")
            {
                Session["PhiltrNumberOfOrders"] = "desc";
                return View("Index", db.Клиенты.OrderBy(a => a.Количество_заказов).ThenBy(o => o.Начало_сотрудничества));
            }
            else
            {
                Session["PhiltrNumberOfOrders"] = "asc";
                return View("Index", db.Клиенты.OrderByDescending(a => a.Количество_заказов).ThenBy(o => o.Начало_сотрудничества));
            }
        }

        public ActionResult FindByName(string name)
        {
            try
            {
                if (name != "")
                    return View("Index", db.Клиенты.Where(o => o.Наименование_Юридического_Лица == name).Select(a => a));
                else
                    return View("Index", db.Клиенты.Select(a => a));
            }
            catch (Exception)
            {
                return View("Index", db.Клиенты.Select(a => a));
            }
        }

    }
}
