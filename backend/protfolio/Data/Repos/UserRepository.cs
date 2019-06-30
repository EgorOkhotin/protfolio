using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Data.Repos
{
    public class UserRepository
    {
        IUserContext _context;
        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task AddUser(User u)
        {
            u.Email = u.Email.ToUpper();
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUser(Expression<Func<User,bool>>  predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<UserSpecializations> GetAllUserSpecializations()
        {
            return _context.UserSpecializations;
        }

        public IQueryable<Profskills> GetAllProfskills()
        {
            return _context.Profskills;
        }

        public IQueryable<User> GetAllUser()
        {
            return _context.Users;
        }

        public async Task UpdateUser(User u)
        {
            _context.Users.Update(u);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<UserContacts>> FindUserContacts(User u)
        {
            return _context.UserContacts.Where(x => x.UserId == u.Id).Include(x => x.Contact);
        }

        public async Task AddUserContact(User u, Contact c)
        {
            var uc = new UserContacts()
            {
                ContactId = c.Id,
                UserId = u.Id
            };

            _context.UserContacts.Add(uc);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<UserSpecializations>> FindUserSpecializations(User u)
        {
            return _context.UserSpecializations.Where(x => x.UserId == u.Id).Include(x => x.Specialization);
        }

        public async Task AddUserSpecialization(User u, Specialization s)
        {
            var us = new UserSpecializations()
            {
                UserId = u.Id,
                SpecializationId = s.Id
            };
            _context.UserSpecializations.Add(us);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Profskills>> FindUserProfSkills(User u)
        {
            return _context.Profskills.Where(x => x.UserId == u.Id);
        }

        public async Task AddUserProfskills(User u, Profskills skill)
        {
            skill.UserId = u.Id;

            _context.Profskills.Add(skill);
            await _context.SaveChangesAsync();
        }
    }

}
