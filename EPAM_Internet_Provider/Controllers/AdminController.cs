using EPAM_Internet_Provider.Domain.Models;
using EPAM_Internet_Provider.Models;
using EPAM_Internet_Provider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace EPAM_Internet_Provider.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly IAccountService _accountService;
        private readonly IRateService _rateService;
        public AdminController(IAccountService accountService, IRateService rateService)
        {
            _accountService = accountService;
            _rateService = rateService;
        }
        public ActionResult Index(UserInfo userinfo)
        {
            return View(userinfo);
        }

        [HttpGet]
        public ActionResult AddUserByAdminSkill()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUserByAdminSkill(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var isExist = await _accountService.IsEmailExist(registerModel.Email);
                if (isExist)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return View(registerModel);
                }
                if (String.Compare(registerModel.Password, registerModel.ConfirmPassword) != 0)
                {
                    ModelState.AddModelError("Password", "The password and confirmation password do not match");
                    return View(registerModel);
                }
                var newUser = new User
                {
                    Name = registerModel.Name,
                    Password = Crypto.Hash(registerModel.Password),
                    Email = registerModel.Email,
                    Role = "Client",
                };
                newUser = await _accountService.AddUser(newUser);
                UserInfo userInfo = new UserInfo
                {
                    Account = newUser.Account,
                    Email = newUser.Email,
                    Name = newUser.Name,
                    Subscributions = newUser.Subscributions,
                    UserId = newUser.UserId,
                    Role = newUser.Role
                };
                return RedirectToAction("RedirectToSuccess", "Admin", userInfo);
            }
            ViewBag.Message = "Invalid Request";
            return View(registerModel);
        }

        [HttpGet]
        public ActionResult RedirectToSuccess(UserInfo userInfo)
        {
            return View(userInfo);
        }

        [HttpGet]
        public async Task<ActionResult> ViewUserList()
        {
            var result = await _accountService.ViewUsersList();
            List<UserInfo> usersInfo = new List<UserInfo>();
            foreach (var user in result)
            {
                UserInfo userInfo = new UserInfo
                {
                    Name = user.Name,
                    Email = user.Email,
                    Account = user.Account,
                    Role = user.Role,
                    Subscributions = user.Subscributions,
                    UserId = user.UserId
                };
                usersInfo.Add(userInfo);
            }
            return View(usersInfo);
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
        public ActionResult EditRateDetails(Rate rate,int rateId)
        {
            //ViewBag.rateName = rate.RateName;
            //ViewBag.rateCost = rate.RateCost;
            //ViewBag.rateId = rate.RateId;
            var NewRate = new Rate
            {
                RateName = rate.RateName,
                RateCost = rate.RateCost,
                RateId = rateId
            };
            var result = _rateService.EditRateById(rate.RateId,rate.RateName,rate.RateCost);
            return RedirectToAction("RateSuccess", NewRate);
        }
        [HttpGet]
        public ActionResult RateSuccess(Rate rate)
        {
            return View(rate);
        }

        public ActionResult ResetPassword(UserInfo userInfo)
        {
            return View(userInfo);
        }
    }
}