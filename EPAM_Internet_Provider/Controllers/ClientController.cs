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

        public ClientController(IAccountService userService,IRateService rateService)
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
            var user=await _accountService.FindUserById(userId);
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
        public async Task<ActionResult>  SelectRates(int serviceId)
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

        public ActionResult ChargeServiceBalance(Service service)
        {
            return View(service);
        }

        public ActionResult ChooseAnotherRate(Service service)
        {
            return View(service);
        }

        public ActionResult UnsubscribeFromRate(Service service)
        {
            return View(service);
        }
        //        // GET: Client/Details/5
        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }
        //
        //        // GET: Client/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }
        //
        //        // POST: Client/Create
        //        [HttpPost]
        //        public ActionResult Create(FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add insert logic here
        //
        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
        //
        //        // GET: Client/Edit/5
        //        public ActionResult Edit(int id)
        //        {
        //            return View();
        //        }
        //
        //        // POST: Client/Edit/5
        //        [HttpPost]
        //        public ActionResult Edit(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add update logic here
        //
        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
        //
        //        // GET: Client/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }
        //
        //        // POST: Client/Delete/5
        //        [HttpPost]
        //        public ActionResult Delete(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add delete logic here
        //
        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
    }
}
