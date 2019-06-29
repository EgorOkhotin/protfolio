using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class ProjectShperes
    {
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Sphere Sphere { get; set; }
        public int SphereId { get; set; }

    }
}
