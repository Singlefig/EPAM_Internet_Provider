using EPAM_Internet_Provider.Domain.Models;
using EPAM_Internet_Provider.Models;
using EPAM_Internet_Provider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EPAM_Internet_Provider.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        readonly IAccountService _accountService;
        private readonly IRateService _rateService;
        public ManagerController(IAccountService accountService, IRateService rateService)
        {
            _accountService = accountService;
            _rateService = rateService;
        }

        public ActionResult Index(UserInfo userinfo)
        {
            return View(userinfo);
        }

        public async Task<ActionResult> GetServices()
        {
            var result = await _rateService.GetAllServices();
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetServiceInfo(int serviceId)
        {
            var result = await _rateService.GetService(serviceId);
            return View(result);
        }
        [HttpGet]
        public ActionResult EditRateDetails(int rateId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditRateDetails(Rate rate, int rateId)
        {
            var NewRate = new Rate
            {
                RateName = rate.RateName,
                RateCost = rate.RateCost,
                RateId = rateId
            };
            var result = _rateService.EditRateById(rate.RateId, rate.RateName, rate.RateCost);
            return RedirectToAction("RateSuccess", NewRate);
        }
        [HttpGet]
        public ActionResult RateSuccess(Rate rate)
        {
            return View(rate);
        }
    }
}