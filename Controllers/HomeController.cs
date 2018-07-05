using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restarter.Models;
using Restarter.Services;

namespace Restarter.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestartTrigger _restartTrigger;
        public HomeController(RestartTrigger restartTrigger)
        {
            _restartTrigger = restartTrigger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            var csServer = new Server
            {
                Name = "cs01"
            };
            var result = await _restartTrigger.Restart(csServer);
            return Json(new { message = result });
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
