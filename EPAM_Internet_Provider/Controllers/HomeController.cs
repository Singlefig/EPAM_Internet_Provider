using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EPAM_Internet_Provider.Domain.Models;
using System.Web.Mvc;
using EPAM_Internet_Provider.Services;
using System.IO;

namespace EPAM_Internet_Provider.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IRateService _rateService;

        public HomeController(IRateService service)
        {
            _rateService = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Services()
        {
            var result = await _rateService.GetAllServices();
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> ServiceInfo(int id)
        {
            var result = await _rateService.GetService(id);
            return View(result);
        }

        [HttpGet]
        public ActionResult DownloadServicesAndRatesFile()
        {
            string file = @"C:\Users\singlefig-ap\Desktop\EPAM\FinalTask\EPAM_Internet_Provider\EPAM_Internet_Provider.DAL\Files\MDNetServicesAndRates.pdf";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }
    }
}