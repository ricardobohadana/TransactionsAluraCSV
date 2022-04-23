using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Infra.Data.Mappings;

namespace TransactionsAluraCSV.Infra.Data.Contexts
{
    public class PostgreSqlContext : DbContext
    {



        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TransferMap());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
    }
}
