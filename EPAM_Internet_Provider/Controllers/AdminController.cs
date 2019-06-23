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
using PagedList;

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
        /// <summary>
        /// Admin Account page
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public ActionResult Index(UserInfo userinfo)
        {
            return View(userinfo);
        }
        /// <summary>
        /// Get method for admin skill add user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUserByAdminSkill()
        {
            return View();
        }
        /// <summary>
        /// Post method for admin skill add user
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get method for redirecting to page when adding user was successful
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RedirectToSuccess(UserInfo userInfo)
        {
            return View(userInfo);
        }
        /// <summary>
        /// Get method for list of users page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ViewUserList(int? page)
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
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(usersInfo.ToPagedList(pageNumber,pageSize));
        }
        /// <summary>
        /// Get method for list of services 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetServices()
        {
            var result = await _rateService.GetAllServices();
            return View(result);
        }
        /// <summary>
        /// Get method for list of rates of current service
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetServiceInfo(int serviceId)
        {
            var result = await _rateService.GetService(serviceId);
            return View(result);
        }
        /// <summary>
        /// Get method for edit rate
        /// </summary>
        /// <param name="rateId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRateDetails(int rateId)
        {
            return View();
        }
        /// <summary>
        /// Post method for edit rate
        /// </summary>
        /// <param name="rateId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRateDetails(Rate rate,int rateId)
        {
            var NewRate = new Rate
            {
                RateName = rate.RateName,
                RateCost = rate.RateCost,
                RateId = rateId
            };
            var result = _rateService.EditRateById(rate.RateId,rate.RateName,rate.RateCost);
            return RedirectToAction("RateSuccess", NewRate);
        }
        /// <summary>
        /// Get method to view changed rate when it was successful
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RateSuccess(Rate rate)
        {
            return View(rate);
        }
        /// <summary>
        /// Get method to block user subscribes by admin skill
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> BlockUser(int userId)
        {
            var user = await _accountService.FindUserById(userId);
            UserInfo userInfo = new UserInfo
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Subscributions = user.Subscributions
            };
            var result = _accountService.BlockUserByAdminSkill(userId);
            return View(userInfo);
        }
        /// <summary>
        /// Get method to unblock user subscribes by admin skill
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> UnblockUser(int userId)
        {
            var user = await _accountService.FindUserById(userId);
            UserInfo userInfo = new UserInfo
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Subscributions = user.Subscributions
            };
            var result = _accountService.UnblockUserByAdminSkill(userId);
            return View(userInfo);
        }
    }
}