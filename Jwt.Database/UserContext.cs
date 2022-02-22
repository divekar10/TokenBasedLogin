using Jwt.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Jwt.Database
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<Register> Register { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
