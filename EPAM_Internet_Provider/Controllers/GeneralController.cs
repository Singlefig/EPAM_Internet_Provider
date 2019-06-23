using EPAM_Internet_Provider.Models;
using EPAM_Internet_Provider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.Controllers
{
    [Authorize]
    public class GeneralController : Controller
    {
        readonly IAccountService _accountService;

        public GeneralController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult EditRatesDetails(List<Rate> rates)
        {
            return View(rates);
        }

        public ActionResult ServiceList()
        {
            return View();
        }
    }
}