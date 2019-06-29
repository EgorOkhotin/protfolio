using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class Profskills
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
    }
}
