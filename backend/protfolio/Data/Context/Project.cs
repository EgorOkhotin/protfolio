using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Opened,
        InProgress,
        Closed
    }
}
