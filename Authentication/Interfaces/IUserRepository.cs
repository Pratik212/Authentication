using System.Threading.Tasks;
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
    }
}