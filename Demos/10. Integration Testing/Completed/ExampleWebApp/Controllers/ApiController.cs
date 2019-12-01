using ExampleWebApp.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWebApp.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly BusinessClass _businessClass;

        public ApiController(BusinessClass businessClass)
        {
            _businessClass = businessClass;
        }

        [HttpGet]
        [Route("business")]
        public string GetBusiness()
        {
            var business = _businessClass.GetTheBusiness();

            return business;
        }
    }
}