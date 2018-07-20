using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using StudentInformationSystem.Business.Services;
using StudentInformationSystem.Entities.ViewModels;

namespace StudentInformationSystem.Web.Controllers
{
    public class CollegeController : Controller
    {
        //
        // GET: /College/

        [HandleError]
        public ActionResult ViewColleges()
        {
            CollegeService collegeService = new CollegeService();
            List<CollegeDetailsModel> lstColleges = new List<CollegeDetailsModel>();
            lstColleges = collegeService.GetColleges();
            return View(lstColleges);
        }

        [HandleError]
        public ActionResult ViewCollege(Guid id)
        {
            CollegeService collegeService = new CollegeService();
            CollegeDetailsModel modelToView = new CollegeDetailsModel();
            modelToView = collegeService.GetCollege(id);
            return View(modelToView);
        }

        [HttpGet]
        [HandleError]
        public ActionResult AddCollege()
        {
            CollegeDetailsModel modelEmpty = new CollegeDetailsModel();
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult AddCollege(CollegeDetailsModel modelToInsert)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            CollegeService collegeService = new CollegeService();
            if (collegeService.InsertCollege(modelToInsert))
            {
                TempData["toast"] = "College Added successfully";
                return RedirectToAction("ViewColleges");
            }
            else
            {
                ModelState.AddModelError("AddCollegeFailedError", "Error in adding the college. The College code should be unique");
                return this.View();
            }
        }

        [HttpGet]
        [HandleError]
        public ActionResult UpdateCollege(Guid id)
        {
            CollegeService collegeService = new CollegeService();
            CollegeDetailsModel modelToDisplay = new CollegeDetailsModel();
            modelToDisplay = collegeService.GetCollege(id);
            return View(modelToDisplay);
        }

        [HttpPost]
        [HandleError]
        public ActionResult UpdateCollege(CollegeDetailsModel modelToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            CollegeService collegeService = new CollegeService();
            if (collegeService.UpdateCollege(modelToUpdate))
            {
                TempData["toast"] = "College Updated successfully";
                return RedirectToAction("ViewColleges");
            }
            else
            {
                ModelState.AddModelError("UpdateCollegeFailedError", "Error in updating the college. The College code should be unique");
                return this.View();
            }
        }

        [HandleError]
        public ActionResult DeleteCollege(Guid id)
        {
            CollegeService collegeService = new CollegeService();
            if (collegeService.DeleteCollege(id))
            {
                TempData["toast"] = "College Deleted successfully";
                return RedirectToAction("ViewColleges");
            }
            else
            {
                ModelState.AddModelError("DeleteCollegeFailedError", "Unable to delete college");
                return this.View();
            }
        }

    }
}
