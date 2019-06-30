using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Data.Repos
{
    public class ProjectRepository
    {
        IProjectContext _context;
        public ProjectRepository(IProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects;
        }

        public async Task AddProject(Project p)
        {
            _context.Projects.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProject(Project p)
        {
            _context.Projects.Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> FindProject(Expression<Func<Project, bool>> predicate)
        {
            return _context.Projects.FirstOrDefault(predicate);
        }

        public async Task<IQueryable<Participant>> FindUserParticipants(User u)
        {
            return _context.Participants.Where(x => x.UserId == u.Id).Include(x => x.Project); 
        }

        public IQueryable<ProjectShperes> GetAllProjectsSpheres()
        {
            return _context.ProjectShperes;
        }

        public async Task<IQueryable<Participant>> FindParticipants(Project p)
        {
            return _context.Participants.Where(x => x.ProjectId == p.Id).Include(x => x.User);
        }

        public async Task AddParticipant(Participant p)
        {
            p.UserId = p.User.Id;
            p.ProjectId = p.Project.Id;
            _context.Participants.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Sphere>> FindProjectSpheres(Project p)
        {
            return _context.ProjectShperes.Where(x => x.ProjectId == p.Id)
                .Include(x => x.Sphere).Select(x => x.Sphere);
        }

        public async Task AddProjectSpheres(Project p, Sphere s)
        {
            var ps = new ProjectShperes()
            {
                ProjectId = p.Id,
                SphereId = s.Id
            };

            _context.ProjectShperes.Add(ps);
            await _context.SaveChangesAsync();
        }

        public async Task AddProjectTag(Project p, string name)
        {
            var pt = new ProjectTags()
            {
                ProjectId = p.Id,
                Name = name
            };

            _context.ProjectTags.Add(pt);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<string>> FindProjectTags(Project p)
        {
            return _context.ProjectTags.Where(x => x.ProjectId == p.Id).Select(x => x.Name);
        }

        public IQueryable<NeedMembers> GetAllNeedMembers()
        {
            return _context.NeedMembers;
        }

        public IQueryable<ProjectTags> GetAllTags()
        {
            return _context.ProjectTags;
        }
    }
}
