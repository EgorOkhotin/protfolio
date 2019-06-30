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
            all = await FilterBySpecialization(all, model);
            all = await FilterByTags(all, model);
            return all;            
        }

        public async Task<IQueryable<User>> FindUser(ProfileSearchModel model)
        {
            var all = _users.GetAllUser();
            if (model == null) return all;

            all = await FilterBySpheresAnsSpecs(all, model);
            all = await FilterByProfSkills(all, model);
            return all;
        }

        private async Task<IQueryable<User>> FilterBySpheresAnsSpecs(IQueryable<User> users, ProfileSearchModel model)
        {
            if (model.SphereId == null) return users;
            if (users.Count() == 0) return users;

            var sphere = (await _spheres.FindSpheres(x => x.Id == model.SphereId)).FirstOrDefault();
            var result = _users.GetAllUserSpecializations()
                .Where(x => x.SphereId == model.SphereId);
            if (model.SpecializationId != null)
                result = result.Where(x => x.SpecializationId == model.SpecializationId);

            return result.Join(users, x => x.UserId, y => y.Id, (x, y) => y);
        }

        private async Task<IQueryable<User>> FilterByProfSkills(IQueryable<User> users, ProfileSearchModel model)
        {
            if (model.ProfSkills == null) return users;
            if (users.Count() == 0) return users;

            var result = _users.GetAllProfskills().Join(users, x => x.UserId, y => y.Id, (x, y) => x)
                .Where(x => model.ProfSkills.Contains(x.Name))
                .Include(x => x.User)
                .Select(x => x.User);
            return result;
        }

        //private async Task<IQueryable<User>> FilterByReadyToWork(IQueryable<User> users, ProfileSearchModel model)
        //{
        //    if (model.SphereId == null) return users;
        //    if (users.Count() == 0) return users;

        //    var result = 
        //}

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
            if (model.SpecializationId == null) return projects;
            if (projects.Count() == 0) return projects;

            var projectJoin = _projects.GetAllNeedMembers()
                .Join(projects, x => x.ProjectId, y => y.Id, (x, y) => x)
                .Where(x => x.SpecializationId == x.SpecializationId)
                .Include(x => x.Project)
                .Select(x => x.Project);

            return projectJoin;
        }

        private async Task<IQueryable<Project>> FilterByTags(IQueryable<Project> projects, ProjectSearchModel model)
        {
            if (model.Tags == null) return projects;
            if (projects.Count() == 0) return projects;

            var result = _projects.GetAllTags()
                .Join(projects, x => x.ProjectId, y => y.Id, (x, y) => x)
                .Where(x => model.Tags.Contains(x.Name))
                .Include(x => x.Project)
                .Select(x => x.Project);
            return result;
        }

    }
}
