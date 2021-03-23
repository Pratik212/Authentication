using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication
{
    public class AuthentiContext : DbContext
    {
        public AuthentiContext(DbContextOptions<AuthentiContext> options) : base(options)
        {
        }
        
        public virtual DbSet<User> Users { get; set; }
    }
}