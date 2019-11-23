using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Example.Models;
using ExampleWebApp.Models;
using ExampleWebApp.Business;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly BusinessClass _businessClass;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            BusinessClass businessClass)
        {
            _logger = logger;
            _businessClass = businessClass;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View(new BusinessViewModel
            {
                BusinessName = _businessClass.GetTheBusiness()
            }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}