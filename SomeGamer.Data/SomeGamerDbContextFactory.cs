using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SomeGamer.Data.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SomeGamer.Data
{
    public class SomeGamerDbContextFactory : IDesignTimeDbContextFactory<SomeGamerDbContext>
    {
        public SomeGamerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../SomeGamer.Api/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<SomeGamerDbContext>();

            //Podemos ter mais de uma connection string
            //Vamos add aqui a conection string com o azure aq
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);

            return new SomeGamerDbContext(builder.Options);

        }
    }
}
