using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NavneVelger.DataContexts;
using NavneVelger.Models;


namespace NavneVelger.Controllers
{
    public class NavnController : Controller
    {
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            NavnViewModel model = new NavnViewModel
            {
                StatusMessage = StatusMessage,
                NavnRangeringList = new List<NavnRangering>()
            };

            


            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
