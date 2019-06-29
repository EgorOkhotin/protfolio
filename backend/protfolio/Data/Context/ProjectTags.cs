using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class ProjectTags
    {
        public int Id { get; set; }
        
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public string Name { get; set; }
    }
}
