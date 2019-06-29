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
            return View();
        }

        [HttpPost]
        public IActionResult ProjectSearch(object some)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult ProfileSearch()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult ProfileSeacrh()
        {
            throw new NotImplementedException();
        }
    }
}