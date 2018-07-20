using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInformationSystem.Entities.ViewModels;
namespace StudentInformationSystem.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {


            Session["RoleID"] = userModel.RoleID;


            if (userModel.RoleID == 0)
            {
                Console.WriteLine("Login failed");
            }
            else if (userModel.RoleID == 1)
            {

                return RedirectToAction("StudentInfo");
            }
            else if (userModel.RoleID == 2)
            {
                return RedirectToAction("ViewStudentInfo");
            }
            return View();
        }

    }
}
