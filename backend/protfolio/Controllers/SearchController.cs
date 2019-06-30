using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using protfolio.Models;
using protfolio.Services;
using protfolio.Data.Repos;
using Microsoft.EntityFrameworkCore;
using protfolio.Data;

namespace protfolio.Controllers
{
    public class SearchController : Controller
    {
        SearchService _search;
        UserRepository _users;
        SpheresRepository _spheres;
        public SearchController(SearchService search, UserRepository users, SpheresRepository sp)
        {
            _search = search;
            _users = users;
            _spheres = sp;
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

            var list = new List<(User, UserSpecializations, Profskills[])>();

            foreach(var u in result)
            {
                var spec = _users.GetAllUserSpecializations().FirstOrDefault(x => x.UserId == u.Id);
                var profSkills = _users.GetAllProfskills().Where(x => x.UserId == u.Id).ToArray();
                list.Add((u, spec, profSkills));
            }
            model.Users = list;
            model.SphereSpecializations = _spheres.GetAllSpecs().Include(x => x.Specialization)
                .Include(x => x.Sphere)
                .AsEnumerable();
            return model;
        }
    }
}