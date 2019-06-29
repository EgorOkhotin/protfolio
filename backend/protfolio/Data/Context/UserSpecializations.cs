using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class UserSpecializations
    {
        public User User { get; set; }
        public int UserId { get; set; } 

        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
    }
}
