using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class SphereSpecializations
    {
        public Sphere Sphere { get; set; }
        public int SphereId { get; set; }

        public Specialization Specialization { get; set; }
	    public int SpecializationId { get; set; }
    }
}
