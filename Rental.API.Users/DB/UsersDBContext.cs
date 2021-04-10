using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.DB
{
    public class UsersDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public UsersDBContext(DbContextOptions options) : base (options)
        {

        }
    }
}
