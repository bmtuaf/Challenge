using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rental.API.Users.DB
{
    public class UsersDBContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }        
        public UsersDBContext(DbContextOptions options) : base (options)
        {

        }
    }
}
