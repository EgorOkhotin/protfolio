﻿using protfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Models
{
    public class ProfileSearchModel
    {
        public int? SphereId { get; set; }
        public int? SpecializationId { get; set; }
        public string[] ProfSkills { get; set; }
        public int? ReadyToWork { get; set; }

        public IEnumerable<(User, UserSpecializations, Profskills[])> Users { get; set; }

        public IEnumerable<SphereSpecializations> SphereSpecializations { get; set; }
    }
}
