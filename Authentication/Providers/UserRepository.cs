using System.Threading.Tasks;
using Authentication.Interfaces;
using Authentication.Models;

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
    }
}