using BRRF.IdentityServer.Repository.DbContexts;
using BRRF.IdentityServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BRRF.IdentityServer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private ApplicationDbContext db;

        public AuthRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public User GetUserById(int id)
        {
            var user = new UserRepository(db).GetAll(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = new UserRepository(db).GetAll(u => String.Equals(u.UserName, username)).FirstOrDefault();
            return user;
        }

        public bool ValidatePassword(string username, string plainTextPassword)
        {
            var user = new UserRepository(db).GetAll(u=>u.UserName==username).FirstOrDefault();
            if (user == null) return false;
            if (String.Equals(plainTextPassword, user.Password)) return true;
            return false;
        }
    }
}
