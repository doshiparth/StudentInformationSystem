using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using StudentInformationSystem.Entities.ViewModels;

using StudentInformationSystem.Entities;

using StudentInformationSystem.Common;

using StudentInformationSystem.Business.Services;

namespace StudentInformationSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: /Home/StudentInfo
        [Authorize(Roles = "Admin, Staff")]
        [HandleError]
        public ActionResult StudentInfo()
        {
            List<StudentInfoModel> newList = new List<StudentInfoModel>();
            newList = StudentServiceTest.GetStudents();
            var check = TempData["toast"];
            return View(newList);
        }

        // Add new Students info 
        // GET: /Home/AddStudentInfo
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult AddStudentInfo()
        {
            //State
            List<State> listState = new List<State>();
            listState = DropdownService.GetState();
            GetState(listState);

            //City
            List<City> listCity = new List<City>();
            listCity = DropdownService.GetCity();
            GetCity(listCity);

            //College
            List<College> listCollege = new List<College>();
            listCollege = DropdownService.GetCollege();
            GetCollege(listCollege);

            //Department
            List<Department> listDepartment = new List<Department>();
            listDepartment = DropdownService.GetDepartment();
            GetDepartment(listDepartment);

            StudentModel student = new StudentModel();
            return View(student);
        }

        // POST: /Home/AddStudentInfo
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult AddStudentInfo(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                ErrorEnums insertCheck = StudentServiceTest.InsertStudent(studentModel);
                string errorString = GetError.GetErrorFromEnum(insertCheck);
                if (errorString.Equals("Added Successfully"))
                {
                    TempData["toastError"] = errorString;
                    return RedirectToAction("StudentInfo");
                }
                else
                {
                    TempData["toastError"] = errorString;
                    return this.View();
                }

                //if (insertCheck == ErrorEnums.Successful)
                //{
                //    TempData["toast"] = "Student Added successfully";
                //    return RedirectToAction("StudentInfo");
                //}
                //else if(insertCheck == ErrorEnums.EmailAddress)
                //{
                //    ModelState.AddModelError("AddFailedError", "Error in adding the student. The Email Address should be unique");
                //    return this.View();
                //}
                //else if(insertCheck == ErrorEnums.PhoneNumber)
                //{
                //    ModelState.AddModelError("AddFailedError", "Error in adding the student. The Contact Number should be unique");
                //    return this.View();
                //}
                //else if(insertCheck == ErrorEnums.Code)
                //{
                //    ModelState.AddModelError("AddFailedError", "Error in adding the student. The Student Code should be unique");
                //    return this.View();
                //}
                //else
                //{
                //    ModelState.AddModelError("AddFailedError", "Error in adding the student. The Student Code, Email Address and the Contact Number should be unique");
                //    return this.View();
                //}
            }
            else
            {
                return this.View();
            }
        }

        // Edit Student Info
        static StudentModel studentToEdit;
        // GET: /Home/EditStudentInfo
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult EditStudentInfo(Guid id)
        {
            //State
            List<State> listState = new List<State>();
            listState = DropdownService.GetState();
            GetState(listState);

            //City
            List<City> listCity = new List<City>();
            listCity = DropdownService.GetCity();
            GetCity(listCity);

            //College
            List<College> listCollege = new List<College>();
            listCollege = DropdownService.GetCollege();
            GetCollege(listCollege);

            //Department
            List<Department> listDepartment = new List<Department>();
            listDepartment = DropdownService.GetDepartment();
            GetDepartment(listDepartment);

            studentToEdit = new StudentModel();
            studentToEdit = StudentServiceTest.GetStudent(id);
            return View(studentToEdit);
        }

        // POST: /Home/EditStudentInfo
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult EditStudentInfo(StudentModel model)
        {
            model.StudentID = studentToEdit.StudentID;
            model.EmailAddress = studentToEdit.EmailAddress;
            model.StudentCode = studentToEdit.StudentCode;

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            if (StudentServiceTest.UpdateStudent(model))
            {
                TempData["toast"] = "Student Edited Successfully";
                return RedirectToAction("StudentInfo");
            }
            else
            {
                ModelState.AddModelError("UpdateFailedError", "Error in updating the student. The Student Code, Email Address and the Contact Number should be unique");
                return this.View();
            }
        }

        // GET: /Home/ViewStudentInfo
        // View Student Info
        [Authorize(Roles = "Admin, Staff, Student")]
        [HandleError]
        public ActionResult ViewStudentInfo(Guid id)
        {
            StudentModel studentModel = StudentServiceTest.GetStudent(id);
            if (studentModel == null)
            {
                return RedirectToAction("StudentInfo");
            }
            TempData["FullName"] = studentModel.FullName;
            ViewBag.Name = studentModel.FirstName;
            return View(studentModel);
        }

        [Authorize(Roles = "Admin, Staff, Student")]
        [HandleError]
        public ActionResult DisplayStudent(string email)
        {
            StudentModel studentModel = StudentServiceTest.GetStudentByEmail(email);
            TempData["FullName"] = studentModel.FullName;
            ViewBag.Name = studentModel.FirstName;
            return RedirectToAction("ViewStudentInfo", new { id = studentModel.StudentID });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteStudent(Guid id)
        {
            if (StudentServiceTest.DeleteStudent(id))
            {
                TempData["toast"] = "Student Deleted Successfully";
                return RedirectToAction("StudentInfo");
            }
            else
            {
                ModelState.AddModelError("DeleteFailedError", "Error in deleting the student. The Administrator has been notified");
                return this.View();
            }
        }

        [Authorize(Roles = "Admin, Staff, Student")]
        public ActionResult ViewStudentHistory(Guid studentId)
        {
            List<StudentHistoryModel> historyList = new List<StudentHistoryModel>();
            historyList = StudentServiceTest.GetStudentHistory(studentId);
           
            ViewBag.Name = TempData["FullName"];
           
            return View(historyList);
        }


        private void GetState(List<State> list)
        {
            HelperModel.SetState(list);
        }

        private void GetCity(List<City> list)
        {
            HelperModel.SetCity(list);
        }
        private void GetCollege(List<College> list)
        {
            HelperModel.SetCollege(list);
        }

        private void GetDepartment(List<Department> list)
        {
            HelperModel.SetDepartment(list);
        }

        //get city list of dropdown from respective state
        [HttpPost]
        public JsonResult GetCityFromId(Guid StateId)
        {
            List<City> citylist = new List<City>();
            citylist = DropdownService.GetCityById(StateId);
            GetCity(citylist);
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }

        //get department list of dropdown from respective college
        [HttpPost]
        public JsonResult GetDepartmentFromId(Guid collegeId)
        {
            List<Department> departmentlist = new List<Department>();
            departmentlist = DropdownService.GetDepartmentById(collegeId);
            GetDepartment(departmentlist);  
            return Json(departmentlist, JsonRequestBehavior.AllowGet);
        }
    }
}
