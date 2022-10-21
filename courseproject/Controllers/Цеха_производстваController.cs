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
    public class Цеха_производстваController : Controller
    {
        private factoryEntities db = new factoryEntities();

        // GET: Цеха_производства
        public ActionResult Index()
        {
            var цеха_производства = db.Цеха_производства.Include(ц => ц.Объекты).Include(ц => ц.Сотрудники);
            return View(цеха_производства.ToList());
        }

        // GET: Цеха_производства/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Цеха_производства цеха_производства = db.Цеха_производства.Find(id);
            if (цеха_производства == null)
            {
                return HttpNotFound();
            }
            return View(цеха_производства);
        }

        // GET: Цеха_производства/Create
        public ActionResult Create()
        {
            ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование");
            //ViewBag.ID_начальника_цеха = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия");
            List<SelectListItem> items = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия").ToList();
            items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_начальника_цеха = items;
            return View();
        }

        // POST: Цеха_производства/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_объекта,Кол_во_сотрудников,ID_начальника_цеха,Общий_объем_инкубаторов,Объем_продукции_в_сутки")] Цеха_производства цеха_производства)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Цеха_производства.Add(цеха_производства);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", цеха_производства.ID_объекта);
                //ViewBag.ID_начальника_цеха = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха);
                List<SelectListItem> items = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха).ToList();
                items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_начальника_цеха = items;
                return View(цеха_производства);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 1 });
            }
        }

        // GET: Цеха_производства/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Цеха_производства цеха_производства = db.Цеха_производства.Find(id);
            if (цеха_производства == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", цеха_производства.ID_объекта);
            //ViewBag.ID_начальника_цеха = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха);
            List<SelectListItem> items = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха).ToList();
            items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
            ViewBag.ID_начальника_цеха = items;
            return View(цеха_производства);
        }

        // POST: Цеха_производства/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_объекта,Кол_во_сотрудников,ID_начальника_цеха,Общий_объем_инкубаторов,Объем_продукции_в_сутки")] Цеха_производства цеха_производства)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(цеха_производства).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_объекта = new SelectList(db.Объекты, "ID_объекта", "Наименование", цеха_производства.ID_объекта);
                //ViewBag.ID_начальника_цеха = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха);
                List<SelectListItem> items = new SelectList(db.Сотрудники, "ID_сотрудника", "Фамилия", цеха_производства.ID_начальника_цеха).ToList();
                items.Insert(0, (new SelectListItem { Text = "null", Value = "" }));
                ViewBag.ID_начальника_цеха = items;
                return View(цеха_производства);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { code = 2 });
            }
        }

        // GET: Цеха_производства/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Цеха_производства цеха_производства = db.Цеха_производства.Find(id);
            if (цеха_производства == null)
            {
                return HttpNotFound();
            }
            return View(цеха_производства);
        }

        // POST: Цеха_производства/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Цеха_производства цеха_производства = db.Цеха_производства.Find(id);
            db.Цеха_производства.Remove(цеха_производства);
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
