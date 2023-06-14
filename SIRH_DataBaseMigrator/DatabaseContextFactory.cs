﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SIRH_DataBase;
using System.IO;

namespace SIRH_DataBaseMigrator
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                               .SetBasePath(path)
                               .AddJsonFile("local.settings.json");

            var configuration = builder.Build();

            //var configuration = new ConfigurationBuilder().AddEnvironmentVariables().AddUserSecrets("LearnToTech.Migration").Build();
            //var configuration = new ConfigurationBuilder().Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(connectionString, x => x.CommandTimeout(3600 * 6)).Options;
            return new DatabaseContext(options);
        }
    }
}
