using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Data.Repos
{
    public class SpheresRepository
    {
        ISpheresContext _context;
        public SpheresRepository(ISpheresContext context)
        {
            _context = context;
        }

        public async Task AddSphere(Sphere s)
        {
            _context.Spheres.Add(s);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Sphere>> GetAllSpheres()
        {
            return _context.Spheres;
        }

        public async Task<IQueryable<Sphere>> FindSpheres(Expression<Func<Sphere, bool>> predicate)
        {
            return _context.Spheres.Where(predicate);
        }

        public async Task<IQueryable<Specialization>> FindSphereSpecializations(Sphere s)
        {
            return _context.SphereSpecializations.Where(x => x.SphereId == s.Id)
                .Include(x => x.Specialization).Select(x => x.Specialization);
        }

        public async Task AddSpecialization(Sphere sphere ,Specialization spec)
        {
            spec = _context.Specializations.Add(spec).Entity;
            var sphereSpec = new SphereSpecializations()
            {
                SpecializationId = spec.Id,
                SphereId = sphere.Id
            };
            _context.SphereSpecializations.Add(sphereSpec);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Specialization> GetAlSpecializations()
        {
            return _context.Specializations;
        }
    }
}
