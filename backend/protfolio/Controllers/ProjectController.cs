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
        public IActionResult Project(int? id)
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

        private async Task<ProjectEditModel> GetProjects(int id)
        {
            var p = _projects.GetAll().FirstOrDefault(x => x.Id == id);
            var list = new List<(Project, Participant[], NeedMembers[])>();
            var participants = (await _projects.FindParticipants(p)).ToArray();
            var needMembers = (_projects.GetAllNeedMembers().Where(x => x.ProjectId == p.Id)
                .Include(x => x.Sphere)
                .Include(x => x.Specialization)).Select(x => x);
            var res = new ProjectEditModel()
            {
                Project = p,
                Participants = participants,
                NeedMembers = needMembers.ToArray(),
            };
            return res;
        }


    }
}