using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Business.Services;

namespace StudentInformationSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult StudentInfo()
        {
            List<StudentInfoModel> newList = StudentService.GetAllStudents();
            //List<StudentInfoModel> newList = new List<StudentInfoModel>();
            //StudentInfoModel stu1 = new StudentInfoModel()
            //{   
            //    FirstName = "abc",
            //    LastName = "pqr" ,
            //    ContactNumber = "9856325417",
            //    EmailAddress = "abc@gmail.com"

            //};

            //StudentInfoModel stu2 = new StudentInfoModel()
            //{

            //    FirstName = "jfh",
            //    LastName = "ghjugh",
            //    ContactNumber = "9876543253",
            //    EmailAddress = "hsdg@jhgf.com"
            //};
            //newList.Add(stu1);
            //newList.Add(stu2);

            return View(newList);


        }

        // Add new Students info 
        public ActionResult AddStudentInfo()
        {
            return View();
        }

        // Edit Student Info
        public ActionResult EditStudentInfo()
        {
            return View();
        }

        // View Student Info
        public ActionResult ViewStudentInfo()
        {
            return View();
        }

    }
}
