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
        /// <summary>
        /// Method to view managet account page
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public ActionResult Index(UserInfo userinfo)
        {
            return View(userinfo);
        }
        /// <summary>
        /// Get method to view service list
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetServices()
        {
            var result = await _rateService.GetAllServices();
            return View(result);
        }
        /// <summary>
        /// Get method to view rate list for current service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetServiceInfo(int serviceId)
        {
            var result = await _rateService.GetService(serviceId);
            return View(result);
        }
        /// <summary>
        /// Get method for edit rate by manager skill
        /// </summary>
        /// <param name="rateId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRateDetails(int rateId)
        {
            return View();
        }
        /// <summary>
        /// Post method for edit rate by manager skill
        /// </summary>
        /// <param name="rateId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get method for view changed rate
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RateSuccess(Rate rate)
        {
            return View(rate);
        }
    }
}