using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using protfolio.Data;
using protfolio.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using protfolio.Models;
using System.Security.Cryptography;

namespace protfolio.Services
{
    public class AuthenticateService
    {
        const int ITERATIONS = 10_000;
        const int PASSWORD_LENGTH = 64;
        UserRepository _repo;
        public AuthenticateService(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClaimsIdentity> Authentificate(string email, string password)
        {
            var user = await FindUser(email);
            if (user == null)
                return null;

            if (!IsRightPassword(user, password))
                return null;

            var identity = GetIdentity(user);
            return identity;
        }

        public async Task<ClaimsIdentity> Registrate(RegisterModel model)
        {
            var isExist = FindUser(model.Email) == null;
            if (isExist) return null;
            var user = MapUser(model);
            byte[] salt = new byte[PASSWORD_LENGTH];
            (RandomNumberGenerator.Create()).GetBytes(salt);
            var passwordBytes = GetPasswordHash(salt, model.Password);

            await _repo.AddUser(user);
            return GetIdentity(user);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String),
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimValueTypes.String);
            return claimsIdentity;
        }

        private async Task<User> FindUser(string email)
        {
            email = email.Normalize();
            return await _repo.FindUser(x => x.Email == email);
        }


        private bool IsRightPassword(User user, string password)
        {
            var hash = GetPasswordHash(user.Salt, password);
            return user.Password.SequenceEqual(hash);
        }

        private byte[] GetPasswordHash(byte[] salt, string password)
        {
            return KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA512,
                ITERATIONS,
                PASSWORD_LENGTH);
        }

        private User MapUser(RegisterModel model)
        {
            var registerDate = DateTime.Now;
            return new User()
            {
                Email = model.Email.Normalize(),
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Gender = model.Gender,

                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                LastVisit = registerDate,
                RegisterDate = registerDate,
            };
        }
    }
}
