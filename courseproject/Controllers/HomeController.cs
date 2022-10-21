using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using courseproject.Models;

namespace courseproject.Controllers
{
    public class HomeController : Controller
    {
        factoryEntities db = new factoryEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMyCapability()
        {
            return View();
        }

        public ActionResult Error (int code)
        {
            if (code == 0)
                ViewBag.Message = "Ошибка добавления расчетов за период.";
            if (code == 1)
                ViewBag.Message = "Ошибка в добавлении новой записи.";
            if (code == 2)
                ViewBag.Message = "Ошибка в изменении записи.";
            if (code == 3)
                ViewBag.Message = "Ошибка в изменении з/п сотрудника.";
            if (code == 4)
                ViewBag.Message = "Ошибка в изменении кол-ва продукции.";
            if (code == 5)
                ViewBag.Message = "Ошибка в изменении информации о себе.";
            if (code == 6)
                ViewBag.Message = "Ошибка в добавлении заказа.";
            return View();
        }
    }
}