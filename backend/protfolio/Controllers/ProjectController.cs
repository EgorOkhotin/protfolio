using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using protfolio.Models;
using protfolio.Data.Repos;
using protfolio.Data;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Controllers
{
    public class ProjectController : Controller
    {
        ProjectRepository _projects;
        public ProjectController(ProjectRepository proj)
        {
            _projects = proj;
        }

        [HttpGet]
        public IActionResult Project()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectEditModel model)
        {
            SaveProject(model);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProject(int? projectId)
        {
            Project proj;
            if(projectId.HasValue)
            {
                proj = await _projects.FindProject(x => x.Id == projectId.Value);
            }
            //create model
            return View();
        }

        [HttpPost]
        public IActionResult EditProject(ProjectEditModel model)
        {
            SaveProject(model);
            return View();
        }

        private bool SaveProject(ProjectEditModel model)
        {
            return false;
        }


    }
}