using FIS_174867.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIS_174867.Controllers
{
    public class AdministratorController : Controller
    {
         static DataBaseEntities Context = new DataBaseEntities();
        public ActionResult Logout()
        {

            Session.Clear();

            return RedirectToAction("Login", "Home");
        }
        // GET: Administrator
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
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
            //string user = Session["UserName"].ToString();
            //var check = Context.Users.Any(u => u.UserName == user);
            //if (check)
            //{
            //    return View();
            //}
            //else
            //    return RedirectToAction("Login","Home" );

        }

        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                var data = Context.Users.ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            //string user = Session["UserName"].ToString();
            //var check = Context.Users.Any(u => u.UserName == user);
            //if (check)
            //{
            //    return View(Context.Users.ToList());
            //}
            //else
            //    return RedirectToAction("Index");


        }
        [HttpPost]
        public ActionResult Index(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Users.ToList()
                            where p.UserName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateFacultyUser()
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
        [HttpPost]
        public ActionResult CreateFacultyUser(User u)
        {
            if (Session["UserName"] != null)
            {
                //PublicationID, FacultyID, PublicationTitle, ArticleName, PublisherName, PublicationLocation, CitationDate
                try
                {
                    DataBaseEntities Context = new DataBaseEntities();
                    if (u.UserRole != null)
                    {
                        if (ModelState.IsValid)
                        {
                            User user = new User();

                            user.UserName = u.UserName;
                            user.Password = u.Password;
                            user.UserRole = u.UserRole;

                            //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                            Context.Users.Add(user);
                            // executes the appropriate commands to implement the changes to the database  
                            Context.SaveChanges();
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        TempData["UserRole"] = "<script>alert('User Role cannot be empty')</script>";
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                { return RedirectToAction("Index"); }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult EditFacultyUser(int? id)
        {
            if (Session["UserName"] != null)
            {
                User user = new User();
                DataBaseEntities Context = new DataBaseEntities();
                user = Context.Users.Find(id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditFacultyUser(User user)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var d = Context.Users.Where(x => x.UsersID == user.UsersID).FirstOrDefault();
                d.UsersID = user.UsersID;
                d.UserName = user.UserName;
                d.UserRole = user.UserRole;

                if (TryUpdateModel(d))
                {
                    Context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult DeleteFacultyUser(int? id)
        {
            if (Session["UserName"] != null)
            {
                User user = new User();
                DataBaseEntities Context = new DataBaseEntities();
                user = Context.Users.Find(id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteFacultyUser(int id)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var fu = Context.Users.Where(x => x.UsersID == id).FirstOrDefault();
                if (fu.UserRole=="Faculty") {
                    var fac = Context.Faculties.Where(x => x.FacultyID == id).FirstOrDefault();
                    Context.Faculties.Remove(fac);
                }
               
               
                Context.Users.Remove(fu);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult IndexDepartment()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Departments.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexDepartment(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Departments.ToList()
                            where p.DeptName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateDepartment()
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
        [HttpPost]
        public ActionResult CreateDepartment(Department dept)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    //DeptID, DeptName
                    if (ModelState.IsValid)
                    {
                        DataBaseEntities Context = new DataBaseEntities();
                        Department department = new Department();
                        //department.DeptID = dept.DeptID;
                        department.DeptName = dept.DeptName;

                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        Context.Departments.Add(department);
                        // executes the appropriate commands to implement the changes to the database  
                        Context.SaveChanges();
                    }
                    else
                    {
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexDepartment");
                }
                catch (Exception ex)
                { return RedirectToAction("IndexDepartment", "Administrator"); }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult EditDept(int? id)
        {
            if (Session["UserName"] != null)
            {
                Department stud = new Department();
                DataBaseEntities Context = new DataBaseEntities();
                stud = Context.Departments.Find(id);
                return View(stud);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditDept(Department department)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var d = Context.Departments.Where(x => x.DeptID == department.DeptID).FirstOrDefault();
                d.DeptID = department.DeptID;
                d.DeptName = department.DeptName;

                if (TryUpdateModel(d))
                {
                    Context.SaveChanges();
                    return RedirectToAction("IndexDepartment");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public ActionResult DeleteDept(int? id)
        {
            if (Session["UserName"] != null)
            {
                Department department = new Department();
                DataBaseEntities Context = new DataBaseEntities();
                department = Context.Departments.Find(id);
                return View(department);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteDept(int id)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var dep = Context.Departments.Where(x => x.DeptID == id).FirstOrDefault();
                Context.Departments.Remove(dep);
                Context.SaveChanges();
                return RedirectToAction("IndexDepartment");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult IndexDesignation()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Designations.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexDesignation(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Designations.ToList()
                            where p.DesignationName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateDesignation()
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
        [HttpPost]
        public ActionResult CreateDesignation(Designation des)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DataBaseEntities Context = new DataBaseEntities();
                        Designation designation = new Designation();
                        //designation.DesignationID = des.DesignationID;
                        designation.DesignationName = des.DesignationName;

                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        Context.Designations.Add(designation);
                        // executes the appropriate commands to implement the changes to the database  
                        Context.SaveChanges();
                    }
                    else { return View(); }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexDesignation");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexDesignation");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult EditDesignation(int? id)
        {
            if (Session["UserName"] != null)
            {
                Designation stud = new Designation();
                DataBaseEntities Context = new DataBaseEntities();
                stud = Context.Designations.Find(id);
                return View(stud);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditDesignation(Designation designation)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var d = Context.Designations.Where(x => x.DesignationID == designation.DesignationID).FirstOrDefault();
                d.DesignationID = designation.DesignationID;
                d.DesignationName = designation.DesignationName;

                if (TryUpdateModel(d))
                {
                    Context.SaveChanges();
                    return RedirectToAction("IndexDesignation");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult DeleteDesignation(int? id)
        {
            if (Session["UserName"] != null)
            {
                Designation designation = new Designation();
                DataBaseEntities Context = new DataBaseEntities();
                designation = Context.Designations.Find(id);
                return View(designation);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteDesignation(int id)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var dep = Context.Designations.Where(x => x.DesignationID == id).FirstOrDefault();
                Context.Designations.Remove(dep);
                Context.SaveChanges();
                return RedirectToAction("IndexDesignation");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult IndexFacultyinfo()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Faculties.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexFacultyinfo(int div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query1 = from p in Context.Faculties.ToList()
                             where p.FacultyID == div
                             select p;
                return View(query1);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult IndexChangeWorkHistory()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.WorkHistories.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexChangeWorkHistory(int div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query1 = from p in Context.WorkHistories.ToList()
                             where p.FacultyID == div
                             select p;
                return View(query1);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult EditWorkHistory(int? id)
        {
            if (Session["UserName"] != null)
            {
                WorkHistory wh = new WorkHistory();
                DataBaseEntities Context = new DataBaseEntities();
                wh = Context.WorkHistories.Find(id);
                return View(wh);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditWorkHistory(WorkHistory workhistory)
        {
            if (Session["UserName"] != null)
            {
                //WorkHistoryID, FacultyID, Organization, JobTitle, JobBeginDate, JobEndDate, JobResponsibilities, JobType
                DataBaseEntities Context = new DataBaseEntities();
                var d = Context.WorkHistories.Where(x => x.WorkHistoryID == workhistory.WorkHistoryID).FirstOrDefault();
                d.WorkHistoryID = workhistory.WorkHistoryID;
                d.FacultyID = workhistory.FacultyID;
                d.Organization = workhistory.Organization;
                d.JobTitle = workhistory.JobTitle;
                d.JobBeginDate = workhistory.JobBeginDate;
                d.JobEndDate = workhistory.JobEndDate;
                d.JobResponsibilities = workhistory.JobResponsibilities;
                d.JobType = workhistory.JobType;

                if (TryUpdateModel(d))
                {
                    Context.SaveChanges();
                    return RedirectToAction("IndexChangeWorkHistory");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult IndexCours()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Courses.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexCours(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Courses.ToList()
                            where p.CourseName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateCours()
        {
            if (Session["UserName"] != null)
            {
                TempData["Dept"] = new SelectList(GetDeptID(), "Value", "Text");
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        private List<SelectListItem> GetDeptID()
        {
            return Context.Departments.Select(e => new SelectListItem
            {
                Value = e.DeptID.ToString(),
                Text = e.DeptName.ToString()
            }).ToList();
        }
        [HttpPost]
        public ActionResult CreateCours(Cours course)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //CourseID, CourseName, CourseCredits, DeptID
                        DataBaseEntities Context = new DataBaseEntities();
                        Cours cours = new Cours();
                       // cours.CourseID = course.CourseID;
                        cours.CourseName = course.CourseName;
                        cours.CourseCredits = course.CourseCredits;
                        cours.DeptID = course.DeptID;

                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        Context.Courses.Add(cours);
                        // executes the appropriate commands to implement the changes to the database  
                        Context.SaveChanges();
                    }
                    else
                    {
                        TempData["Dept"] = new SelectList(GetDeptID(), "Value", "Text");
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexCours");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexCours");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult EditCours(int? id)
        {
            if (Session["UserName"] != null)
            {
                Cours c = new Cours();
                DataBaseEntities Context = new DataBaseEntities();
                c = Context.Courses.Find(id);
                return View(c);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditCours(Cours cours)
        {
            if (Session["UserName"] != null)
            {
                //CourseID, CourseName, CourseCredits, DeptID
                DataBaseEntities Context = new DataBaseEntities();
                var c = Context.Courses.Where(x => x.CourseID == cours.CourseID).FirstOrDefault();
                c.CourseID = cours.CourseID;
                c.CourseName = cours.CourseName;
                c.CourseCredits = cours.CourseCredits;
                c.DeptID = cours.DeptID;

                if (TryUpdateModel(c))
                {
                    Context.SaveChanges();
                    return RedirectToAction("IndexCours");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult DeleteCours(int? id)
        {
            if (Session["UserName"] != null)
            {
                Cours cours = new Cours();
                DataBaseEntities Context = new DataBaseEntities();
                cours = Context.Courses.Find(id);
                return View(cours);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteCours(int id)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var cours = Context.Courses.Where(x => x.CourseID == id).FirstOrDefault();
                Context.Courses.Remove(cours);
                Context.SaveChanges();
                return RedirectToAction("IndexCours");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult IndexSubject()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                return View(Context.Subjects.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult IndexSubject(string div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query = from p in Context.Subjects.ToList()
                            where p.SubjectName == div
                            select p;
                return View(query);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateSubject()
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
        [HttpPost]
        public ActionResult CreateSubject(Subject subject)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //SubjectID, SubjectName
                        DataBaseEntities Context = new DataBaseEntities();
                        Subject sub = new Subject();
                       // sub.SubjectID = subject.SubjectID;
                        sub.SubjectName = subject.SubjectName;


                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        Context.Subjects.Add(sub);
                        // executes the appropriate commands to implement the changes to the database  
                        Context.SaveChanges();
                    }
                    else { return View(); }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexSubject");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexSubject");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult EditSubject(int? id)
        {
            if (Session["UserName"] != null)
            {
                Subject s = new Subject();
                DataBaseEntities Context = new DataBaseEntities();
                s = Context.Subjects.Find(id);
                return View(s);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditSubject(Subject subject)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var s = Context.Subjects.Where(x => x.SubjectID == subject.SubjectID).FirstOrDefault();
                s.SubjectID = subject.SubjectID;
                s.SubjectName = subject.SubjectName;


                if (TryUpdateModel(s))
                {
                    Context.SaveChanges();
                    return RedirectToAction("IndexSubject");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult DeleteSubject(int? id)
        {
            if (Session["UserName"] != null)
            {
                Subject subject = new Subject();
                DataBaseEntities Context = new DataBaseEntities();
                subject = Context.Subjects.Find(id);
                return View(subject);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteSubject(int id)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var sub = Context.Subjects.Where(x => x.SubjectID == id).FirstOrDefault();
                Context.Subjects.Remove(sub);
                Context.SaveChanges();
                return RedirectToAction("IndexSubject");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }

}