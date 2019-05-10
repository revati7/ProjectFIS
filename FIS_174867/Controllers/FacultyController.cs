using FIS_174867.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIS_174867.Controllers
{
    public class FacultyController : Controller
    {
        static DataBaseEntities Context = new DataBaseEntities();
        public ActionResult Logout()
        {
            
            Session.Clear();
           
            return RedirectToAction("Login", "Home");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Home()
        {
            if(Session["UserName"]!= null)
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
            //    return RedirectToAction("Index");

        }
        // GET: Faculty
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                string user = Session["UserName"].ToString();
                int i = Convert.ToInt32(Session["UserID"]);
                var check = Context.Users.Any(u => u.UserName == user);
                if (check)
                {
                    return View(Context.Publications.Where(l => l.FacultyID == i).ToList());
                }
                else
                    return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
           
        }
        [HttpPost]
        public ActionResult Index(int div)
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                var query1 = from p in Context.Publications.ToList()
                             where p.FacultyID == div
                             select p;
                return View(query1);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreatePublication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePublication(Publication pub)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //PublicationID, FacultyID, PublicationTitle, ArticleName, PublisherName, PublicationLocation, CitationDate
                        DataBaseEntities Context = new DataBaseEntities();
                        Publication publication = new Publication();
                        //publication.PublicationID = pub.PublicationID;
                        publication.FacultyID = Convert.ToInt32(Session["UserID"]);
                        publication.PublicationTitle = pub.PublicationTitle;
                        publication.ArticleName = pub.ArticleName;
                        publication.PublisherName = pub.PublisherName;
                        publication.PublicationLocation = pub.PublicationLocation;
                        publication.CitationDate = pub.CitationDate;

                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        Context.Publications.Add(publication);
                        // executes the appropriate commands to implement the changes to the database  
                        Context.SaveChanges();
                    }
                    else { return View(); }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult EditPublication(int? id)
        {
            if (Session["UserName"] != null)
            {
                Publication publication = new Publication();
                DataBaseEntities Context = new DataBaseEntities();
                publication = Context.Publications.Find(id);
                return View(publication);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditPublication(Publication publication)
        {
            if (Session["UserName"] != null)
            {
                //PublicationID, FacultyID, PublicationTitle, ArticleName, PublisherName, PublicationLocation, CitationDate
                DataBaseEntities Context = new DataBaseEntities();
                var p = Context.Publications.Where(x => x.FacultyID == publication.FacultyID).FirstOrDefault();
                p.PublicationID = publication.PublicationID;
                p.FacultyID = Convert.ToInt32(Session["UserID"]);
                p.PublicationTitle = publication.PublicationTitle;
                p.ArticleName = publication.ArticleName;
                p.PublisherName = publication.PublisherName;
                p.PublicationLocation = publication.PublicationLocation;
                p.CitationDate = publication.CitationDate;

                if (TryUpdateModel(p))
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult Indexinfo()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                int i = Convert.ToInt32(Session["UserID"]);
                return View(Context.Faculties.Where(l => l.FacultyID == i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult Indexinfo(int div)
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
        public ActionResult PersonalInfo()
        {
            if (Session["UserName"] != null)
            {
                TempData["Dept"] = new SelectList(GetDeptID(), "Value", "Text");
                TempData["Designation"] = new SelectList(GetDesignationID(), "Value", "Text");
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
        private List<SelectListItem> GetDesignationID()
        {
            return Context.Designations.Select(e => new SelectListItem
            {
                Value = e.DesignationID.ToString(),
                Text = e.DesignationName.ToString()
            }).ToList();
        }
        [HttpPost]
        public ActionResult PersonalInfo(Faculty fa)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    //FacultyID, FirstName, LastName, Address, City, State, Pincode, MobileNo, HireDate, EmailAddress, DateofBirth, DeptID, DesignationID
                    if (ModelState.IsValid)
                    {
                        DataBaseEntities piContext = new DataBaseEntities();
                        Faculty faculty = new Faculty();

                        faculty.FacultyID =Convert.ToInt32(Session["UserID"]);
                        faculty.FirstName = fa.FirstName;
                        faculty.LastName = fa.LastName;
                        faculty.Address = fa.Address;
                        faculty.City = fa.City;
                        faculty.State = fa.State;
                        faculty.Pincode = fa.Pincode;
                        faculty.MobileNo = fa.MobileNo;
                        faculty.HireDate = fa.HireDate;
                        faculty.EmailAddress = fa.EmailAddress;
                        faculty.DateofBirth = fa.DateofBirth;
                        faculty.DeptID = fa.DeptID;
                        faculty.DesignationID = fa.DesignationID;


                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        piContext.Faculties.Add(faculty);
                        // executes the appropriate commands to implement the changes to the database  
                        piContext.SaveChanges();
                    }
                    else { return View(); }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("Indexinfo");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Indexinfo");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] != null)
            {
                TempData["Dept"] = new SelectList(GetDeptID(), "Value", "Text");
                TempData["Designation"] = new SelectList(GetDesignationID(), "Value", "Text");
                Faculty faculty = new Faculty();
                DataBaseEntities Context = new DataBaseEntities();
                faculty = Context.Faculties.Find(id);
                return View(faculty);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(Faculty faculty)
        {
            if (Session["UserName"] != null)
            {
                //FirstName, LastName, Address, City, State, Pincode, MobileNo, HireDate, EmailAddress, DateofBirth, DeptID, DesignationID
                DataBaseEntities Context = new DataBaseEntities();
                var f = Context.Faculties.Where(x => x.FacultyID == faculty.FacultyID).FirstOrDefault();
                f.FirstName = faculty.FirstName;
                f.LastName = faculty.LastName;
                f.Address = faculty.Address;
                f.City = faculty.City;
                f.State = faculty.State;
                f.Pincode = faculty.Pincode;
                f.MobileNo = faculty.MobileNo;
                f.HireDate = faculty.HireDate;
                f.EmailAddress = faculty.EmailAddress;
                f.DateofBirth = faculty.DateofBirth;
                f.DeptID = faculty.DeptID;
                f.DesignationID = faculty.DesignationID;
                if (TryUpdateModel(f))
                {
                    Context.SaveChanges();
                    return RedirectToAction("Indexinfo");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult IndexWorkHistory()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                int i = Convert.ToInt32(Session["UserID"]);
                return View(Context.WorkHistories.Where(l => l.FacultyID == i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateWorkHistory()
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
        public ActionResult CreateWorkHistory(WorkHistory workhistory)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var check = Context.WorkHistories.Where(x => x.JobBeginDate == workhistory.JobBeginDate).FirstOrDefault();
                        if(check == null)
                        {
                            //WorkHistoryID, FacultyID, Organization, JobTitle, JobBeginDate, JobEndDate, JobResponsibilities, JobType
                            DataBaseEntities jContext = new DataBaseEntities();
                            WorkHistory wh = new WorkHistory();
                            // wh.WorkHistoryID = workhistory.WorkHistoryID;
                            wh.FacultyID = Convert.ToInt32(Session["UserID"]);
                            wh.Organization = workhistory.Organization;
                            wh.JobTitle = workhistory.JobTitle;
                            wh.JobBeginDate = workhistory.JobBeginDate;
                            wh.JobEndDate = workhistory.JobEndDate;
                            wh.JobResponsibilities = workhistory.JobResponsibilities;
                            wh.JobType = workhistory.JobType;

                            //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                            jContext.WorkHistories.Add(wh);
                            // executes the appropriate commands to implement the changes to the database  
                            jContext.SaveChanges();
                            TempData["message"] = "Created Successfully";
                        }
                        else
                        {
                            TempData["message"] = "Joining date not valid!";
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexWorkHistory");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexWorkHistory");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult IndexCourseTaught()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                int i = Convert.ToInt32(Session["UserID"]);
                return View(Context.CoursesTaughts.Where(l => l.FacultyID == i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateCourseTaught()
        {
            if (Session["UserName"] != null)
            {
                TempData["Subject"] = new SelectList(GetSubjectID(), "Value", "Text");
                TempData["Course"] = new SelectList(GetCourseID(), "Value", "Text");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        private List<SelectListItem> GetSubjectID()
        {
            return Context.Subjects.Select(e => new SelectListItem
            {
                Value = e.SubjectID.ToString(),
                Text = e.SubjectName.ToString()
            }).ToList();
        }
        private List<SelectListItem> GetCourseID()
        {
            return Context.Courses.Select(e => new SelectListItem
            {
                Value = e.CourseID.ToString(),
                Text = e.CourseName.ToString()
            }).ToList();
        }
        [HttpPost]
        public ActionResult CreateCourseTaught(CoursesTaught coursestaught)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //CourseID, FacultyID, SubjectID, FirstDateTaught
                        DataBaseEntities jContext = new DataBaseEntities();
                        CoursesTaught ct = new CoursesTaught();
                        ct.CourseID = coursestaught.CourseID;
                        ct.FacultyID = Convert.ToInt32(Session["UserID"]); 
                        ct.SubjectID = coursestaught.SubjectID;
                        ct.FirstDateTaught = coursestaught.FirstDateTaught;


                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        jContext.CoursesTaughts.Add(ct);
                        // executes the appropriate commands to implement the changes to the database  
                        jContext.SaveChanges();
                    }
                    else
                    {
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexCourseTaught");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexCourseTaught");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult IndexGrant()
        {
            if (Session["UserName"] != null)
            {
                DataBaseEntities Context = new DataBaseEntities();
                int i = Convert.ToInt32(Session["UserID"]);
                return View(Context.Grants.Where(l => l.FacultyID == i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult CreateGrant()
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
        public ActionResult CreateGrant(Grant grant)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //GrantID, FacultyID, GrantTitle, GrantDescription
                        DataBaseEntities jContext = new DataBaseEntities();
                        Grant g = new Grant();
                        //g.GrantID = grant.GrantID;
                        g.FacultyID = Convert.ToInt32(Session["UserID"]); 
                        g.GrantTitle = grant.GrantTitle;
                        g.GrantDescription = grant.GrantDescription;


                        //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                        jContext.Grants.Add(g);
                        // executes the appropriate commands to implement the changes to the database  
                        jContext.SaveChanges();
                    }
                    else
                    {
                        return View();
                    }
                    ViewBag.Message = "Successfully Added";
                    return RedirectToAction("IndexGrant");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("IndexGrant");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

    }
}