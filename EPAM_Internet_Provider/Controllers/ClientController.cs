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

        public async Task<ActionResult> Index()
        {
            //            if (WebSecurity.IsAuthenticated)
            //            {
            //                //Accesses the username
            //                string currentUserName = WebSecurity.CurrentUserName;
            //            }
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

        [HttpGet]
        public async Task<ActionResult> AddSubscription()
        {
            var result = await _rateService.GetAllServices();
            //            var info = new AddSubscriptionInfo
            //            {
            //                UserId = userId,
            //                AvilableServices = result
            //            };
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> SelectRates(int serviceId)
        {
            ViewBag.serviceId = serviceId;
            var result = await _rateService.GetRatesForService(serviceId);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Subscribe(int rateId, int serviceId)
        {
            var userId = (int)HttpContext.Session["UserId"];
            await _rateService.SubscribeUser(userId, serviceId, rateId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChargeServiceBalance(Subscription subscription)
        {
            var sub = _rateService.FindSubscribeBySubId(subscription.SubscriptionId);
            return View(subscription);
        }
        [HttpGet]
        public ActionResult ChargeService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChargeService(Subscription sub)
        {
            var result = _rateService.ChargeSubscribe(sub.SubscriptionId, sub.ServiceBalance);
            return RedirectToAction("Index");
        }

        public ActionResult ChooseAnotherRate(Service service)
        {
            return View(service);
        }
        [HttpGet]
        public ActionResult Unsubscribe(Subscription subscription)
        {
            return View(subscription);
        }

        [HttpPost]
        public ActionResult Unsubscribe(int subId)
        {
            var result = _accountService.UnsubscribeUser(subId);
            return RedirectToAction("Index");
        }
    }
}
