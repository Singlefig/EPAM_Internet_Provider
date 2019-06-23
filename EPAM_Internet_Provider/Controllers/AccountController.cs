using System;
using EPAM_Internet_Provider.Domain;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using EPAM_Internet_Provider.Domain.Models;
using EPAM_Internet_Provider.Models;
using EPAM_Internet_Provider.Services;

namespace EPAM_Internet_Provider.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignUp(RegisterModel registerModel)
        {
            // Model Validation 
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
                return RedirectByRole(newUser);
            }
            ViewBag.Message = "Invalid Request";
            return View(registerModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignIn(LoginModel userModel)
        {
            // Model Validation 
            if (ModelState.IsValid)
            {
                var user = await _accountService.FindUserByEmail(userModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("EmailExist", "Email doesn't exist");
                    return View(userModel);
                }
                string pass = Crypto.Hash(userModel.Password);
                if (String.Compare(pass, user.Password, StringComparison.Ordinal) != 0)
                {
                    ModelState.AddModelError("Wrong password", "Password is not correct");
                    return View(userModel);
                }
                FormsAuthentication.SetAuthCookie(userModel.Email, userModel.RememberMe);
                return RedirectByRole(user);
            }
            ViewBag.Message = "Invalid Request";
            return View(userModel);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        private ActionResult RedirectByRole(User user)
        {
            if (HttpContext.Session["UserId"] == null)
            {
                HttpContext.Session["UserId"] = user.UserId;
            }

            if (user.Role == "Admin")
                return RedirectToAction("Index", "Admin");

            if (user.Role == "Manager")
                return RedirectToAction("Index", "Manager");

            if (user.Role == "Client")
                return RedirectToAction("Index","Client");

            ViewBag.Message = "Invalid Role";
            return View(user);
        }
    }
}
