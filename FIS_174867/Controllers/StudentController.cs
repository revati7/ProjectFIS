using FIS_174867.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIS_174867.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Logout()
        {
            //Session["UserName"] = null;
            //Session["UserID"] = null;
            Session.Abandon();
            //Session["UserRole"] = null;
            return RedirectToAction("Login", "Home");
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

       [OutputCache(NoStore = true, Duration = 0, VaryByParam ="None")]
        public ActionResult Home()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Publicationsbyarticlename()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Publications.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult Publicationsbyarticlename(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Publications.ToList()
                            where p.ArticleName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}