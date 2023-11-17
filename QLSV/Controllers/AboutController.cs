using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Controllers
{
    public class AboutController : Controller
    {
        [Route("about.html", Name = "AboutUs")]
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}
