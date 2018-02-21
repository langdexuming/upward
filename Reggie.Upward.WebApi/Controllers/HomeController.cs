using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Reggie.Upward.WebApi.Controllers
{
    [EnableCors("AllowSameDomain")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}