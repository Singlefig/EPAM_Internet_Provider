using EPAM_Internet_Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM_Internet_Provider.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index(UserInfo userinfo)
        {
            return View(userinfo);
        }
    }
}