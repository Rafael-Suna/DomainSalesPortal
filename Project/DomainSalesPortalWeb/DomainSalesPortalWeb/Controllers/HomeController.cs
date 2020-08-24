using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DomainSalesPortalWeb.Models;
using DomainSalesPortalWeb.Services;
using Newtonsoft.Json;

namespace DomainSalesPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Favourite()
        {
            return View();
        }


        public JsonResult GetDomainInformation(string domain)
        {

            var result = RDapService.SearchDomain(domain);
            DomainSearchVM entity = null;
            if (result !=null)
            {
                 entity = JsonConvert.DeserializeObject<DomainSearchVM>(result.ToString());

                
            }

            return Json(entity);
        }



        public JsonResult AddFavourite(string domain)
        {

            return Json("");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
