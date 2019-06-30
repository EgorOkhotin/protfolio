using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using protfolio.Models;
using protfolio.Services;
using protfolio.Data.Repos;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Controllers
{
    public class SearchController : Controller
    {
        SearchService _search;
        UserRepository _users;
        public SearchController(SearchService search, UserRepository users)
        {
            _search = search;
            _users = users;
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
            var model = await GetProfile(null);
            return View("ProfileSearch", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileSeacrh(ProfileSearchModel model)
        {
            model = await GetProfile(model);
            return View("ProfileSearch", model);
        }

        private async Task<ProfileSearchModel> GetProfile(ProfileSearchModel model)
        {
            var result = await _search.FindUser(model);
            var specs = _users.GetAllUserSpecializations().Join(result, x => x.UserId, y => y.Id, (x, y) => x)
                .Include(x => x.User)
                .Include(x => x.Sphere)
                .Include(x => x.Specialization);
            model.Users = result.AsEnumerable();
            model.Specializations = specs;
            return model;
        }
    }
}