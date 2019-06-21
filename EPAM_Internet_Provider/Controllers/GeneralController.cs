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
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (String.Compare(resetPasswordModel.NewPassword, resetPasswordModel.ConfirmNewPassword) != 0)
            {
                ModelState.AddModelError("Password", "The password and confirmation password do not match");
                return View(resetPasswordModel);
            }
            return View();
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