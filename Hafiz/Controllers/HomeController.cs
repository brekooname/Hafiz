using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hafiz.Models;

namespace Hafiz.Controllers
{
    public class HomeController : Controller
    {
        private static string[] SurahNameCache;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Arrange()
        {
            if (SurahNameCache == null)
            {
                SurahNameCache = new string[114];
                for (int i = 0; i < MemorizeController.AlQuran.Suwar.Count; i++)
                {
                    SurahNameCache[i] = MemorizeController.AlQuran.Suwar[i + 1].EN_Name;
                }
            }
            return View(SurahNameCache);
        }

        public IActionResult Quiz()
        {
            if (SurahNameCache == null)
            {
                SurahNameCache = new string[114];
                for (int i = 0; i < MemorizeController.AlQuran.Suwar.Count; i++)
                {
                    SurahNameCache[i] = MemorizeController.AlQuran.Suwar[i + 1].EN_Name;
                }
            }
            return View(SurahNameCache);
        }

        public IActionResult FillInTheBlanks()
        {
            if (SurahNameCache == null)
            {
                SurahNameCache = new string[114];
                for (int i = 0; i < MemorizeController.AlQuran.Suwar.Count; i++)
                {
                    SurahNameCache[i] = MemorizeController.AlQuran.Suwar[i + 1].EN_Name;
                }
            }
            return View(SurahNameCache);
        }

        
        public IActionResult Read()
        {
            if (SurahNameCache == null)
            {
                SurahNameCache = new string[114];
                for (int i = 0; i < MemorizeController.AlQuran.Suwar.Count; i++)
                {
                    SurahNameCache[i] = MemorizeController.AlQuran.Suwar[i + 1].EN_Name;
                }
            }
            
            return View(SurahNameCache);
        }

        [HttpPost]
        public IActionResult Arrange(int surah, int type, int? fromRange, int? toRange)
        {
            if (type == 1)
            {
                //Arrange entire surah
                return RedirectToAction("ArrangeSurah", "Memorize", new { surah = surah });
            } else if (type == 2)
            {
                //Arrange custom range
                if (fromRange == null || toRange == null)
                {
                    return StatusCode(404);
                }
                return RedirectToAction("ArrangeSurahRange", "Memorize", new { surah = surah, begin = fromRange.Value, end = toRange.Value });
            } 
            return StatusCode(404);
            
        }
    }
}
