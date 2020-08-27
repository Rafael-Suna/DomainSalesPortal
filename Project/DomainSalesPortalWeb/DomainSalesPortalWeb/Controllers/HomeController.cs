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
using Microsoft.Extensions.Caching.Memory;

namespace DomainSalesPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private IMemoryCache _memoryCache;
        UnitOfWork _uwo;

        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration , IMemoryCache memoryCache)
        {
            _logger = logger;
            _configuration = Configuration;
            _memoryCache = memoryCache;

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
            ViewBag.UserName = sessionContext;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.UserName = "";
            return RedirectToAction("Login");
        }   

        public IActionResult Favourite()
        {
            
                int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));


                var result = _uwo.DomainRepository.FindByCustomerId(UserId);

        
                return View(result);
        
   
          
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

                    HttpContext.Session.SetString("UserId", result.Id.ToString());

                    _memoryCache.Set("UserId", result.Id.ToString());
                    
                    return RedirectToAction("Index");
                }


    

            }

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterCustomerVM newRecord)
        {

            if (ModelState.IsValid)
            {


                try
                {

                    newRecord.RegisteredDate = DateTime.Now;
                    newRecord.FullName = newRecord.Name + newRecord.Surname;

                    Customer newCustomer = new Customer()
                    {
                        Name = newRecord.Name,
                        Surname = newRecord.Surname,
                        Email = newRecord.Email,
                        FullName = newRecord.FullName,
                        Password = newRecord.Password,
                        RegisteredDate = newRecord.RegisteredDate
                    };




                    _uwo.CustomerRepository.Add(newCustomer);
                    _uwo.Commit();
                }
                catch (Exception ex)
                {

                    throw;
                }



                return RedirectToAction("Login");

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

            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            Domain newRecord = new Domain()
            {

                CustomerId = UserId,
                IsActive = true,
                Name = FavouriteData.ldhName,
                NS1 = FavouriteData.nameservers[0].ldhName,
                NS2 = FavouriteData.nameservers[1].ldhName,
                IsFavourite = true,
                LastChange = FavouriteData.events[2].eventDate,
                ExpiredDate = FavouriteData.events[1].eventDate



            };


            try
            {

                var FavouriteDataCheck = _uwo.DomainRepository.FindByCustomerId(UserId).Where(x => x.Name == newRecord.Name).FirstOrDefault();

                if (FavouriteDataCheck == null)
                {
                    _uwo.DomainRepository.Add(newRecord);
                    _uwo.Commit();
                }

                return Json("Success");



            }
            catch (Exception ex)
            {

                return Json(ex.Message.ToString());
            }

        }


        [HttpPost]
        public JsonResult RemoveFavourite(string Domains)
        {
            try
            {
                var result =  Domains.Split(",");

                foreach (var item in result)
                {

                    var domainId = Convert.ToInt32(item.ToString());


                    _uwo.DomainRepository.Remove(domainId);
                    _uwo.Commit();
                }
            
                return Json(Domains);


             
            }
            catch (Exception ex)
            {

                return Json("Error");
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
