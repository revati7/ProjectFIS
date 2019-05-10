using FIS_174867.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIS_174867.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login() { 
        //{ Session["UserID"] = null;
        //        Session["UserName"] = null;
        //        Session["UserRole"] = null;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
           
            if (ModelState.IsValid)
            {
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["UserRole"] = null;
                using (DataBaseEntities db = new DataBaseEntities())
                {
                    User obj = db.Users.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UsersID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["UserRole"] = obj.UserRole.ToString();
                        if (obj.UserRole.ToLower() == "faculty")
                        {
                            return RedirectToAction("Home", "Faculty");
                        }
                        else if (obj.UserRole.ToLower() == "administrator")
                        {
                            return RedirectToAction("Home", "Administrator");
                        }
                        else if (obj.UserRole.ToLower() == "student")
                        {
                            return RedirectToAction("Home", "Student");
                        }
                        else
                        {
                            ViewBag.JavaScript = "<script language='javascript' type='text/javascript'>alert('Invalid Username')";
                            return RedirectToAction("Login");
                        }

                    }
                }
            }
            return View(objUser);
        }


        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Publicationsbyarticlename()
        //{
        //    DataBaseEntities Context = new DataBaseEntities();
        //    return View(Context.Publications.ToList());
        //}
        //[HttpPost]
        //public ActionResult Publicationsbyarticlename(string div)
        //{
        //    DataBaseEntities Context = new DataBaseEntities();
        //    var query = from p in Context.Publications.ToList()
        //                where p.ArticleName == div
        //                select p;
        //    return View(query);
        //}
      
    }
}