using protfolio.Data;
using protfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Models
{
    public class ProjectSearchModel
    {
        public int? SphereId { get; set; }
        public string Name { get; set; }
        public int? SpecializationId { get; set; }
        public string[] Tags { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        public bool IsEmpty()
        {
            return SphereId == null &&
                Name == null &&
                SpecializationId == null &&
                Tags == null;
        }


    }
}
