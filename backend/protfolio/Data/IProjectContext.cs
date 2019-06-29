using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public interface IProjectContext : IContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Participant> Participants { get; set; }
        DbSet<ProjectShperes> ProjectShperes { get; set; }
        DbSet<ProjectTags> ProjectTags { get; set; }
    }
}
