using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public class UserContacts
    {
        public Contact Contact { get; set; }
        public int ContactId { get; set; }

        public User User { get; set; }
	    public int UserId { get; set; }

	    public string Value { get; set; }
    }
}
