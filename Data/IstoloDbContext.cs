using iStolo1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace iStolo1.Data
{
    public class IstoloDbContext : DbContext
    {
        public IstoloDbContext(DbContextOptions<IstoloDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

}

