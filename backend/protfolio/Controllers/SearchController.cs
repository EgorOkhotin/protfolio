using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using protfolio.Models;
using protfolio.Services;

namespace protfolio.Controllers
{
    public class SearchController : Controller
    {
        SearchService _search;
        public SearchController(SearchService search)
        {
            _search = search;
        }
        [HttpGet]
        public async Task<IActionResult> ProjectSearch()
        {
            var res = new ProjectSearchModel()
            {
                Projects = (await _search.FindProjects(null)).AsEnumerable()
            };
            return View("ProjectSearch", res);
        }

        [HttpPost]
        public async Task<IActionResult> ProjectSearch(ProjectSearchModel some)
        {
            var result = await _search.FindProjects(some);
            some.Projects = result.AsEnumerable();
            return View("ProjectSearch", some);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileSearch()
        {
            var res = await _search.FindUser(null);
            var model = new ProfileSearchModel() { Users = res.AsEnumerable() };
            return View("ProfileSearch", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileSeacrh(ProfileSearchModel model)
        {
            var result = await _search.FindUser(model);
            model.Users = result.AsEnumerable();
            return View("ProfileSearch", model);
        }
    }
}