using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInformationSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Shared/PageNotFoundError
        public ActionResult PageNotFoundError()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

    }
}
