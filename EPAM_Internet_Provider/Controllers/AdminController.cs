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
        public AdminController(IAccountService accountService)
        {
            _accountService = accountService;
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

        public ActionResult ResetPassword(UserInfo userInfo)
        {
            return View(userInfo);
        }
    }
}