using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hafiz.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int id)
        {
            return View(id);
        }

        public IActionResult Display(int id)
        {
            return View("Index", id);
        }
    }
}