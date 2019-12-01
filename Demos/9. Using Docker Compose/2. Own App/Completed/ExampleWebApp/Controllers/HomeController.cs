using Example.Models;
using ExampleWebApp.Business;
using ExampleWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly BusinessClass _businessClass;

        public HomeController(BusinessClass businessClass)
        {
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
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}