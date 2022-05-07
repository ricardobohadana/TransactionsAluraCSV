using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAluraCSV.Infra.Data.Contexts
{
    public class PostgreSqlMigration : IDesignTimeDbContextFactory<PostgreSqlContext>
    {
        public PostgreSqlContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            //var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            var connectionstring = root.GetSection("ConnectionStrings").GetSection("Postgres").Value;
            var builder = new DbContextOptionsBuilder<PostgreSqlContext>();
            builder.UseNpgsql(connectionstring);
            return new PostgreSqlContext(builder.Options);
        }
    }
}
