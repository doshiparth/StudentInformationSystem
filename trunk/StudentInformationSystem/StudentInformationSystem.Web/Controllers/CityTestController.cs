using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInformationSystem.Business.Services;
using StudentInformationSystem.Entities.ViewModels;
using StudentInformationSystem.Entities;


namespace StudentInformationSystem.Web.Controllers
{
    public class CityTestController : Controller
    {
        //
        // GET: /CityTest/
        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult AddNewCity()
        {
            List<State> listState = new List<State>();
            listState = DropdownService.GetState();
            GetState(listState);
            CityModel cityModel = new CityModel();
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCity(CityModel cityModel)
        {
            CityTestService cityService = new CityTestService();
            if (ModelState.IsValid)
            {
                if (cityService.InsertCity(cityModel))
                    return RedirectToAction("ViewStates", "CityTest");
                else
                    ModelState.AddModelError("AddCityFailedError", "Error in adding the city");
                    return this.View();
                   // return RedirectToAction("AddCity");
            }
            return View();
         }
        
        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult ViewStates()
        {

            CityTestService cityTestService = new CityTestService();
            List<StateModel> lstState = new List<StateModel>();
            lstState = cityTestService.GetStates();
            return View(lstState);

        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult ViewCities(Guid StateID)
        {
            CityTestService cityTestService = new CityTestService();
            //CityModel cityModel = new CityModel();
            var cityModel = cityTestService.GetCities(StateID);
            return View(cityModel);


        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult DeleteCity(Guid CityID)
        {
            CityTestService cityTest = new CityTestService();
            cityTest.DeleteCity(CityID);
            return RedirectToAction("ViewStates");
        }


        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult UpdateCity(Guid CityID)
        {
            CityTestService cityService = new CityTestService();
            CityModel cityModel = new CityModel();
            cityModel = cityService.GetCity(CityID);
            return View(cityModel);
        }
       
        [HttpPost]
        public ActionResult UpdateCity(CityModel cityModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            CityTestService cityTest = new CityTestService();
            if(cityTest.UpdateCity(cityModel))
                return RedirectToAction("ViewCities", "CityTest", new { StateID = cityModel.StateID });
            else
            {
                ModelState.AddModelError("UpdateCityFailedError", "Error in updating the city.");
                return this.View();
            }

        }

         private void GetState(List<State> list)
        {
            HelperModel.SetState(list);
        }


    }
}
