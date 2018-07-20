using System;
using System.Web;
using System.Web.Mvc;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Business.Services;
using System.Web.Security;
using System.Security.Cryptography;

using StudentInformationSystem.Entities;

namespace StudentInformationSystem.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {

        // GET: /Login/
        [HttpGet]
        [HandleError]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult Login(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            //MD5 md5Hash = MD5.Create();
            //string encryptedPassword = Encryptor.GetMd5Hash(md5Hash, userModel.Password);

            int RoleID = UserServiceTest.Login(userModel.EmailAddress, userModel.Password);
            

            if (RoleID == 0)
            {
                //Console.WriteLine("Login failed");
                ModelState.AddModelError("LoginFailedError", "Invalid login attempt.");
                return View(userModel);
            }
            
            else if (RoleID == 1)
            {
                userModel.Role = "Admin";
                PerformFormAuthentication(userModel);
            
                return RedirectToAction("StudentInfo","Home");
            }
            else if (RoleID == 2)
            {
                userModel.Role = "Staff";
                PerformFormAuthentication(userModel);

                return RedirectToAction("StudentInfo", "Home");
            }
            else if (RoleID == 3)
            {
                userModel.Role = "Student";
                PerformFormAuthentication(userModel);

                return RedirectToAction("DisplayStudent", "Home", new { email = userModel.EmailAddress });
            }
            return View();
        }

        private void PerformFormAuthentication(UserModel userModel)
        {
            Session["Role"] = userModel.Role;
            FormsAuthentication.SetAuthCookie(userModel.EmailAddress, false);
            var authTicket = new FormsAuthenticationTicket(1, userModel.EmailAddress, DateTime.Now, DateTime.Now.AddMinutes(20), false, userModel.Role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }

        [HandleError]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login", "Login");
        }

    }
}