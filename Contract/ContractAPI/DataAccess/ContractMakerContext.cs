
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
        public DbSet<ClientCompanyInfo> ClientCompanyInfo { get; set; }
        public DbSet<CreateContractInfo> CreateContractInfo { get; set; }
        public DbSet<ServicesInfo> ServicesInfo { get; set; }
        public DbSet<PurposeOfContract> PurposeOfContracts { get; set; }
        public DbSet<UnapprovedContract> UnapprovedContracts { get; set; }
        public DbSet<ApplicableContract> ApplicableContracts { get; set; }
        public DbSet<CanceledContract> CanceledContracts { get; set; }
        public DbSet<AboutApp> AboutApp { get; set; }

        private DbContextOptions _options { get; set; }
        public ContractMakerContext(DbContextOptions options) : base(options)
        {
            this._options = options;
        }

        public ContractMakerContext CreateNew()
        {
            return new ContractMakerContext(this._options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(r => r.phone_number);

            modelBuilder.Entity<UserCompanyInfo>().ToTable("UserCompanyInfo");
            modelBuilder.Entity<UserCompanyInfo>().HasKey(r => r.created_date);

            modelBuilder.Entity<ClientCompanyInfo>().ToTable("ClientCompanyInfo");
            modelBuilder.Entity<ClientCompanyInfo>().HasKey(r => r.created_date);

            modelBuilder.Entity<CreateContractInfo>().ToTable("CreateContractInfo");
            modelBuilder.Entity<CreateContractInfo>().HasKey(r => r.created_date);

            modelBuilder.Entity<ServicesInfo>().ToTable("ServicesInfo");
            modelBuilder.Entity<ServicesInfo>().HasKey(r => r.created_date);

            modelBuilder.Entity<PurposeOfContract>().ToTable("PurposeOfContracts");
            modelBuilder.Entity<PurposeOfContract>().HasKey(r => r.user_phone_number);

            modelBuilder.Entity<UnapprovedContract>().ToTable("UnapprovedContracts");
            modelBuilder.Entity<UnapprovedContract>().HasKey(r => r.date_of_contract);

            modelBuilder.Entity<ApplicableContract>().ToTable("ApplicableContracts");
            modelBuilder.Entity<ApplicableContract>().HasKey(r => r.date_of_contract);

            modelBuilder.Entity<CanceledContract>().ToTable("CanceledContracts");
            modelBuilder.Entity<CanceledContract>().HasKey(r => r.date_of_contract);

            modelBuilder.Entity<AboutApp>().ToTable("AboutApp");
            modelBuilder.Entity<AboutApp>().HasKey(r => r.lan_code);
        }
    }
}
