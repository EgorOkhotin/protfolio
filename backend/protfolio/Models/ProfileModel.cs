using protfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Models
{
    public class ProfileModel
    {
        public User User { get; set; }

        public Profskills[] Profskills { get; set; }
        public Participant[] Participants { get; set; }
        public UserContacts[] Contacts { get; set; }

    }
}
