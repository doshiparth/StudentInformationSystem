using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Entities;
using StudentInformationSystem.Business.Services;

namespace StudentInformationSystem.Web.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult GetDepartment()
        {
            List<College> listCollege = new List<College>();
            listCollege = DropdownService.GetCollege();
            GetCollege(listCollege);
            return View();
        }

        //[HttpPost]
        //[HandleError]
        //public ActionResult GetDepartment(CollegeModel collegeModel)
        //{
        //    DepartmentService departmentService = new DepartmentService();
        //    departmentService.GetDepartment(collegeModel);
        //    return RedirectToAction("GetAllDepartment", "Department");
        //}


        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpPost]
        public ActionResult GetDepartment(Guid CollegeID)
        {

            CollegeModel collegemodel = new CollegeModel();
            collegemodel.CollegeID = CollegeID;
            return RedirectToAction("GetAllDepartment", "Department", collegemodel);
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult GetAllDepartment(CollegeModel collegemodel)
        {
            CollegeModel collegemodelobj = new CollegeModel();
            DepartmentService departmentService = new DepartmentService();
            List<CollegeModel> allDepartment = new List<CollegeModel>();
            allDepartment = departmentService.GetAllDepartment(collegemodel.CollegeID);
            return View(allDepartment);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult AddDepartment()
        {
            List<College> listCollege = new List<College>();
            listCollege = DropdownService.GetCollege();
            GetCollege(listCollege);
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult AddDepartment(CollegeModel collegeModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            DepartmentService departmentService = new DepartmentService();
            if (departmentService.InsertDepartment(collegeModel))
            {
                TempData["toast"] = "Department Data Inserted Successfully";
                //ViewBag.result = "Record Inserted Successfully!";
                return RedirectToAction("GetDepartment");
            }
            else
            {
                ModelState.AddModelError("AddDepartmentFailedError", "Error in adding the Department. The Department code should be unique");
                return this.View();
            }
        }

        [HttpGet]
        [HandleError]
        public ActionResult EditDepartment(Guid id)
        {
            DepartmentService departmentService = new DepartmentService();
            CollegeModel collegeModel = new CollegeModel();
            collegeModel = departmentService.GetDepartment(id);
            return View(collegeModel);
        }

        [HttpPost]
        [HandleError]
        public ActionResult EditDepartment(CollegeModel collegeModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            DepartmentService departmentService = new DepartmentService();
            if (departmentService.EditDepartment(collegeModel))
            {
                TempData["toast"] = "Department Data Updated Successfully";
                return RedirectToAction("GetAllDepartment", "Department", collegeModel);
            }
            else
            {
                ModelState.AddModelError("UpdateDepartmentFailedError", "Error in updating the college. The Department code should be unique characters");
                return this.View();
            }
            //DepartmentService departmentService = new DepartmentService();
            //departmentService.EditDepartment(collegeModel);
            //return RedirectToAction("GetDepartment");
        }

        [HandleError]
        public ActionResult DeleteDepartment(Guid id, Guid clgid)
        {
            DepartmentService departmentService = new DepartmentService(); ;
            departmentService.DeleteDepartment(id);
            CollegeModel collegeModel = new CollegeModel();
            collegeModel.CollegeID = clgid;
            return RedirectToAction("GetAllDepartment", "Department", collegeModel);
        }

        private void GetCollege(List<College> list)
        {
            HelperModel.SetCollege(list);
        }
    }
}
