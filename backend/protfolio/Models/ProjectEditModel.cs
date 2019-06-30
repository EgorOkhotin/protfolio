using protfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Models
{
    public class ProjectEditModel
    {
        public Project Project { get; set; }
        public Participant[] Participants { get; set; }
        public NeedMembers[] NeedMembers { get; set; }
    }
}
