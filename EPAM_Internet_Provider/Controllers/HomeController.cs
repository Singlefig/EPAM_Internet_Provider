using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EPAM_Internet_Provider.Domain.Models;
using System.Web.Mvc;
using EPAM_Internet_Provider.Services;
using System.IO;
using PagedList;

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
        public async Task<ActionResult> ServiceInfo(int id, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "RateName" : "";
            ViewBag.CostSortParm = sortOrder == "RateCost" ? "RateCost" : "RateCost_Desc";
            var result = await _rateService.GetService(id);
            switch (sortOrder)
            {
                case "RateName":
                    {
                        result.Rates = result.Rates.OrderBy(s => s.RateName).ToList();
                    }
                    break;
                case "RateName_Desc":
                    {
                        result.Rates = result.Rates.OrderByDescending(s => s.RateName).ToList();
                    }
                    break;
                case "RateCost":
                    {
                        result.Rates = result.Rates.OrderBy(s => s.RateCost).ToList();
                    }
                    break;
                case "RateCost_Desc":
                    {
                        result.Rates = result.Rates.OrderByDescending(s => s.RateCost).ToList();
                    }
                    break;
                default:
                    {
                        result.Rates.OrderBy(s => s.RateName);
                    }
                    break;
            }
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