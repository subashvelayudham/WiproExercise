using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Data.Models;

namespace User.Data.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }

        public DbSet<USER> Users { get; set; }
        public DbSet<ADDRESS> Addresses { get; set; }
        public DbSet<EMPLOYMENT> Employments { get; set; }
        public DbSet<USER_EMPLOYMENT> User_Employments { get; set; }

    }
}
