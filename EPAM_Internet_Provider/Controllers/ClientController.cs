using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPAM_Internet_Provider.Domain.Models;
using EPAM_Internet_Provider.Models;
using EPAM_Internet_Provider.Services;

namespace EPAM_Internet_Provider.Controllers
{
    public class AddSubscriptionInfo
    {
        public int UserId { get; set; }
        public IEnumerable<Service> AvilableServices { get; set; }
    }

    [Authorize]
    public class ClientController : Controller
    {
        private readonly IRateService _rateService;
        private readonly IAccountService _accountService;

        public ClientController(IAccountService userService, IRateService rateService)
        {
            _rateService = rateService;
            _accountService = userService;
        }
        /// <summary>
        /// Method to view client account page
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            int userId = (int)HttpContext.Session["UserId"];
            var user = await _accountService.FindUserById(userId);
            var userInfo = new UserInfo
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Account = user.Account,
                Role = user.Role,
                Subscributions = user.Subscributions
            };

            return View(userInfo);
        }
        /// <summary>
        /// Get method to add subscribe
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> AddSubscription()
        {
            var result = await _rateService.GetAllServices();
            return View(result);
        }
        /// <summary>
        /// Get method to select rate for subscribe
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SelectRates(int serviceId)
        {
            ViewBag.serviceId = serviceId;
            var result = await _rateService.GetRatesForService(serviceId);
            return View(result);
        }
        /// <summary>
        /// Get method to subscribe
        /// </summary>
        /// <param name="rateId"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Subscribe(int rateId, int serviceId)
        {
            var userId = (int)HttpContext.Session["UserId"];
            await _rateService.SubscribeUser(userId, serviceId, rateId);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get method to charge service balance in current subscribe
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ChargeServiceBalance(int subId)
        {
            var sub = await _rateService.FindSubscribeBySubId(subId);
            return View(sub);
        }
        /// <summary>
        /// Post method to charge service balance in current subscribe
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChargeServiceBalance(int subId,Subscription subscription)
        {
            var result = _rateService.ChargeSubscribe(subId,subscription.ServiceBalance);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Get method to unsubscribe 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Unsubscribe(Subscription subscription)
        {
            return View(subscription);
        }
        /// <summary>
        /// Post method to unsubscribe 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Unsubscribe(int subId)
        {
            var result = _accountService.UnsubscribeUser(subId);
            return RedirectToAction("Index");
        }
    }
}
