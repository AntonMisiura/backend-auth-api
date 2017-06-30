using AuthAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
#if DEBUG
            return RedirectPermanent("/swagger/ui/index");
#endif
            return View();
        }
    }
}
