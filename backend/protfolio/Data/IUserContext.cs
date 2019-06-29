using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace protfolio.Data
{
    public interface IUserContext : IContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<UserContacts> UserContacts { get; set; }
        DbSet<UserSpecializations> UserSpecializations { get; set; }
        DbSet<Profskills> Profskills { get; set; }
    }
}
