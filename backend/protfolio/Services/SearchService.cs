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
            if (model.SphereIds == null) return projects;
            if (projects.Count() == 0) return projects;
            var projectSpheres = _projects.GetAllProjectsSpheres().Where(x => model.SphereIds.Contains(x.SphereId))
                .Include(x => x.Project)
                .Where(x => projects.Contains(x.Project))
                .Select(x => x.ProjectId);
            return projects.Where(x => projectSpheres.Contains(x.Id));
        }

        private async Task<IQueryable<Project>> FilterBySphereSpecializtionPairs(IQueryable<Project> projects, ProjectSearchModel model)
        {
            if (model.SphereSpecializtionPairs == null) return projects;

            var specs = model.GetSpecialization().Select(x => x.Value);

            // projects.Where
            return null;

        }
    }
}
