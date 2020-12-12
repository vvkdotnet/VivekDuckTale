using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivekDuckTale.Models;


namespace VivekDuckTale.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetStudents(string Search, int? SubjectID)
        {
            using (StudentDataContext dc = new StudentDataContext())
            {
                var details = (from s in dc.Student
                               join su in dc.Subject on s.SubjectID equals  su.ID  
                               
                               select new StudentVM()
                               {
                                   ID = s.ID,
                                   SubjectID = s.SubjectID,
                                   FirstName = s.FirstName,
                                   LastName = s.LastName,
                                   Class = s.Class,
                                   Marks = s.Marks,
                                   SubjectName = su.SubjectName
                               }).ToList();
                
                    details = details.Where(s => (s.FirstName.Contains(Search) || s.LastName.Contains(Search)) && (SubjectID == null || SubjectID == 0 || s.SubjectID == SubjectID)).ToList();
                
                return Json(new { details }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetSubjects()
        {
            using (StudentDataContext dc = new StudentDataContext())
            {
                var Subjects = dc.Subject.ToList();
                return Json(new { Subjects }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetStudentDetail(int ID)
        {
            using (StudentDataContext dc = new StudentDataContext())
            {
                var details = (from s in dc.Student
                               join su in dc.Subject on s.SubjectID equals su.ID where s.ID == ID

                               select new StudentVM()
                               {
                                   ID = s.ID,
                                   FirstName = s.FirstName,
                                   LastName = s.LastName,
                                   Class = s.Class,
                                   Marks = s.Marks,
                                   SubjectName = su.SubjectName
                               }).FirstOrDefault();
                return Json(new { details }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SaveStudent(StudentVM Model)
        {
            using (StudentDataContext dc = new StudentDataContext())
            {
                var student = dc.Student.Where(s => s.ID == Model.ID).FirstOrDefault();
                var subject = dc.Subject.Where(s => s.SubjectName.ToLower() == Model.SubjectName.ToLower()).FirstOrDefault();
                if (subject == null)
                {
                    subject = new Subject();
                    subject.SubjectName = Model.SubjectName;
                    dc.Subject.Add(subject);
                    dc.SaveChanges();
                }
               
                if (student == null)
                {
                    student = new Student();
                    dc.Student.Add(student);
                }
                student.FirstName = Model.FirstName;
                student.LastName = Model.LastName;
                student.Class = Model.Class;
                student.SubjectID = subject.ID;
                student.Marks = Model.Marks;
                dc.SaveChanges();
            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteStudent(int ID)
        {
            using (StudentDataContext dc = new StudentDataContext())
            {
                var student = dc.Student.Where(s => s.ID == ID).FirstOrDefault();
                dc.Student.Remove(student);
                dc.SaveChanges();
            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public class StudentVM
        {
            public int ID { get; set; }
            public int SubjectID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Class { get; set; }
            public string Marks { get; set; }
            public string SubjectName { get; set; }
        }
    }
}