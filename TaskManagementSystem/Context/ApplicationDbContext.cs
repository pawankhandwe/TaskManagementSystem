
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Tasks> Tasks { get; set; }
        //public DbSet<Register> Register { get; set; }


    }
}
