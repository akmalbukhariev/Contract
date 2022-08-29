using LibContract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibContract.DataAccess
{
    public class ContractMakerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ContractMakerContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
