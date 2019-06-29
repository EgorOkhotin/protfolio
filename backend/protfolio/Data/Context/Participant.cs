using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class Participant
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Project Project { get; set; }
	    public int ProjectId { get; set; }

        public bool IsOwner { get; set; }

	    public string Role { get; set; }

	    public string Description { get; set; }
    }
}
