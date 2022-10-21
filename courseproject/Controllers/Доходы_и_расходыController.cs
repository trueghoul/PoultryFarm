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
    public class Доходы_и_расходыController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Доходы_и_расходы
        public ActionResult Index()
        {
            Session["PhiltrFinalSum"] = "asc";
            return View(db.Доходы_и_расходы.ToList());
        }

        // GET: Доходы_и_расходы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доходы_и_расходы доходы_и_расходы = db.Доходы_и_расходы.Find(id);
            if (доходы_и_расходы == null)
            {
                return HttpNotFound();
            }
            return View(доходы_и_расходы);
        }

        // GET: Доходы_и_расходы/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Доходы_и_расходы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_периода,Положительный,Итоговая_сумма,Прибыль_от_продаж,Затраты_на_комбикорм,Затраты_на_электроэнергию,Затраты_на_з_п_сотрудников,Затраты_на_другое,Дата_учёта")] Доходы_и_расходы доходы_и_расходы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Доходы_и_расходы.Add(доходы_и_расходы);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(доходы_и_расходы);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Доходы_и_расходы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доходы_и_расходы доходы_и_расходы = db.Доходы_и_расходы.Find(id);
            if (доходы_и_расходы == null)
            {
                return HttpNotFound();
            }
            return View(доходы_и_расходы);
        }

        // POST: Доходы_и_расходы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_периода,Положительный,Итоговая_сумма,Прибыль_от_продаж,Затраты_на_комбикорм,Затраты_на_электроэнергию,Затраты_на_з_п_сотрудников,Затраты_на_другое,Дата_учёта")] Доходы_и_расходы доходы_и_расходы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(доходы_и_расходы).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(доходы_и_расходы);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Доходы_и_расходы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доходы_и_расходы доходы_и_расходы = db.Доходы_и_расходы.Find(id);
            if (доходы_и_расходы == null)
            {
                return HttpNotFound();
            }
            return View(доходы_и_расходы);
        }

        // POST: Доходы_и_расходы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Доходы_и_расходы доходы_и_расходы = db.Доходы_и_расходы.Find(id);
            db.Доходы_и_расходы.Remove(доходы_и_расходы);
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

        public ActionResult Income(int? electr)
        {
            try
            {
                db.IncomeAndExpenses(electr);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 0 });
            }
        }

        public ActionResult PhiltrFinalSum()
        {
            if(Session["PhiltrFinalSum"].ToString() == "asc")
            {
                Session["PhiltrFinalSum"] = "desc";
                return View("Index", db.Доходы_и_расходы.OrderBy(a => a.Итоговая_сумма).ThenBy(o => o.ID_периода));
            }
            else
            {
                Session["PhiltrFinalSum"] = "asc";
                return View("Index", db.Доходы_и_расходы.OrderByDescending(a => a.Итоговая_сумма).ThenBy(o => o.ID_периода));
            }
        }
    }
}
