using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using protfolio.Data.Repos;
using protfolio.Data;
using protfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Services
{
    public class SearchService
    {
        UserRepository _users;
        ProjectRepository _projects;
        SpheresRepository _spheres;

        public SearchService(UserRepository users, ProjectRepository proj, SpheresRepository spheres)
        {
            _users = users;
            _projects = proj;
            _spheres = spheres;
        }

        public async Task<IQueryable<Project>> FindProjects(ProjectSearchModel model)
        {
            var all = _projects.GetAll();
            if (model == null)
                return all;

            if (model.Name != null)
                all = all.Where(x => EF.Functions.Like(x.Name, model.Name));

            all = await FilterBySpheres(all, model);
            return null;
            
        }

        private async Task<IQueryable<Project>> FilterBySpheres(IQueryable<Project> projects, ProjectSearchModel model)
        {
            if (model.SphereId == null) return projects;
            if (projects.Count() == 0) return projects;

            var projectJoin = _projects.GetAllProjectsSpheres()
                .Where(x => x.SphereId == model.SphereId)
                .Join(projects, x => x.ProjectId, p => p.Id, (p, x) => x);
            return projectJoin;
        }

        private async Task<IQueryable<Project>> FilterBySpecialization(IQueryable<Project> projects, ProjectSearchModel model)
        {
            if (model.SphereId == null) return projects;
            if (projects.Count() == 0) return projects;

            var projectJoin = _projects.GetAllNeedMembers()
                .Where(x => x.SpecializationId == x.SpecializationId)
                .Join(projects, x => x.ProjectId, y => y.Id, (x, y) => y);

            return projectJoin;
        }

    }
}
