
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
        public DbSet<UserCompanyInfo> UserCompanyInfo { get; set; }
        public DbSet<PurposeOfContract> PurposeOfContract { get; set; }

        public ContractMakerContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(r => r.id);

            modelBuilder.Entity<UserCompanyInfo>().ToTable("UserCompanyInfo");
            modelBuilder.Entity<UserCompanyInfo>().HasKey(r => r.user_phone_number);

            modelBuilder.Entity<PurposeOfContract>().ToTable("PurposeOfContract");
            modelBuilder.Entity<PurposeOfContract>().HasKey(r => r.user_phone_number);
        }
    }
}
