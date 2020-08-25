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
using DomainSalesPortalDataLayer;
using Microsoft.Extensions.Configuration;
using DomainSalesPortalDataLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace DomainSalesPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        UnitOfWork _uwo;

        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;


           string connectionString = _configuration.GetConnectionString("Constr");
            _uwo = new UnitOfWork(connectionString);
        }

        public IActionResult Index()
        {

            var checkKey = HttpContext.Session.Keys.ToList();

            if (checkKey.Count == 0)
            {
                

                return RedirectToAction("Login");
            }

            var sessionContext = HttpContext.Session.GetString("User");

            if (sessionContext == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Favourite()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel record)
        {

            if (ModelState.IsValid)
            {
                var result = _uwo.CustomerRepository.Login(record.Email, record.Password);


                if (result != null)
                {
                    HttpContext.Session.SetString("User", $"{result.Name} - {result.Surname}");
                }


                return View();

            }

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


      
        public JsonResult AddFavourite(DomainSearchVM FavouriteData)
        {


            Domain newRecord = new Domain()
            {
                CustomerId=1,
                IsActive = true,
                Name = FavouriteData.ldhName,
                NS1 = FavouriteData.nameservers[0].ldhName,
                NS2 = FavouriteData.nameservers[1].ldhName,
                IsFavourite = true,
                LastChange=FavouriteData.events[1].eventDate,
                ExpiredDate = FavouriteData.events[2].eventDate



            };


            try
            {
                _uwo.DomainRepository.Add(newRecord);
                _uwo.Commit();
            }
            catch (Exception ex)
            {

                throw;
            }



            //using (var uow = new UnitOfWork("LosGatos"))
            //{





            //    uow.DomainRepository.Add.Insert(breed1);
            //    uow.BreedRepository.Insert(breed2);
            //    cat1.BreedId = breed1.BreedId;
            //    cat2.BreedId = breed1.BreedId;
            //    cat3.BreedId = breed2.BreedId;
            //    uow.CatRepository.Insert(cat1);
            //    uow.CatRepository.Insert(cat2);
            //    uow.CatRepository.Insert(cat3);
            //    uow.SaveChanges();
            //}

            return Json("");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
