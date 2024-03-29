﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using protfolio.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace protfolio.Models
{
    //[Authorize]
    public class ProfileController : Controller
    {
        UserRepository _users;

        ProjectRepository _projects;
        public ProfileController(UserRepository users, ProjectRepository projects)
        {
            _users = users;
            _projects = projects;
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int? id)
        {
            var model = new ProfileModel();
            if (!id.HasValue)
                model = await GetProfileModel();
            else model = await GetProfileModel(id.Value);
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Profile()
        //{
            
        //}
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var userEmail = HttpContext.User.Identity.Name.NormalizeString();

            var u = await _users.FindUser(x => x.Email == userEmail);
            await _users.UpdateUser(u);

            model = await GetProfileModel();
            return View(model);
        }

        private async Task<ProfileModel> GetProfileModel(int id)
        {
            var userEmail = HttpContext.User.Identity.Name.NormalizeString();

            var user = _users.GetAllUser().FirstOrDefault(x => x.Id == id);
            var participants = await _projects.FindUserParticipants(user);
            var skills = await _users.FindUserProfSkills(user);
            var contacts = await _users.FindUserContacts(user);
            var userSpecialization = _users.GetAllUserSpecializations()
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Sphere)
                .Include(x => x.Specialization)
                .FirstOrDefault();

            var model = new ProfileModel()
            {
                User = user,
                Participants = participants.ToArray(),
                Profskills = skills.ToArray(),
                Contacts = contacts.ToArray(),
                Specialization = userSpecialization
            };
            return model;
        }

        private async Task<ProfileModel> GetProfileModel()
        {
            var userEmail = HttpContext.User.Identity.Name.NormalizeString();

            var user = _users.GetAllUser().First();
            var participants = await _projects.FindUserParticipants(user);
            var skills = await _users.FindUserProfSkills(user);
            var contacts = await _users.FindUserContacts(user);
            var userSpecialization = _users.GetAllUserSpecializations()
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Sphere)
                .Include(x => x.Specialization)
                .FirstOrDefault();

            var model = new ProfileModel()
            {
                User = user,
                Participants = participants.ToArray(),
                Profskills = skills.ToArray(),
                Contacts = contacts.ToArray(),
                Specialization = userSpecialization
            };
            return model;
        }

    }
}
