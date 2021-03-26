using System;
using System.Threading.Tasks;
using Authentication.Helpers;
using Authentication.Interfaces;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Providers
{
    /// <summary>
    /// UserRepository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AuthentiContext _context;

        /// <summary>
        /// UserRepository
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(AuthentiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Register(User user)
        {
            var userObj = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return userObj.Entity;
        }

        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<User> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);

            // check if username exists
            if (user == null)
                return null;

            if (user.PasswordHash == null && user.PasswordSalt == null)
                return null;
            // check if password is correct
            return !Utils.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }
    }
}