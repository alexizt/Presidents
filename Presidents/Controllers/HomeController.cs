using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presidents.Controllers
{
    public class HomeController : Controller
    {
        // Comentario rama prueba
        // Comentario 2 rama prueba
        public ActionResult Index()
        {
            ViewBag.Title = "US Presidents - Home";

            return View();
        }
    }
}
