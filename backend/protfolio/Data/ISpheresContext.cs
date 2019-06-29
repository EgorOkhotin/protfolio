using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public interface ISpheresContext : IContext
    {
        DbSet<Sphere> Spheres { get; set; }
        DbSet<Specialization> Specializations { get; set; }
        DbSet<SphereSpecializations> SphereSpecializations { get; set; }
    }
}
