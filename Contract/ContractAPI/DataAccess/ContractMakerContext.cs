
using ContractAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractAPI.DataAccess
{
    public class ContractMakerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ContractMakerContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(r => r.id);
        }
    }
}
