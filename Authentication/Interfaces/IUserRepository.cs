using System.Threading.Tasks;
using Authentication.Dtos;
using Authentication.Models;

namespace Authentication.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> Register(User user);
        
        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email);
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login(string email, string password);

        /// <summary>
        /// GetUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserId(long userId);
    }
}