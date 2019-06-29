using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace protfolio.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public IActionResult ProjectSearch()
        {
            return View("ProjectSearch");
        }

        [HttpPost]
        public IActionResult ProjectSearch(object some)
        {
            return View("ProjectSearch");
        }

        [HttpGet]
        public IActionResult ProfileSearch()
        {
            return View("ProfileSearch");
        }

        [HttpPost]
        public IActionResult ProfileSeacrh()
        {
            return View("ProfileSearch");
        }
    }
}