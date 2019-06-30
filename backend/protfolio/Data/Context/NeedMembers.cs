using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class NeedMembers
    {
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Sphere Sphere { get; set; }
        public int SphereId { get; set; }

        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
