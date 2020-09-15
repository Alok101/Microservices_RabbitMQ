using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Transfer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    public class TransferFactory : IDesignTimeDbContextFactory<TransferDbContext>
    {
        //// Get environment
        //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //// Build config
        //IConfiguration config = new ConfigurationBuilder()
        //    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EfDesignDemo.Web"))
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .AddJsonFile($"appsettings.{environment}.json", optional: true)
        //    .AddEnvironmentVariables()
        //    .Build();
        public TransferDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            var optionsBuilder = new DbContextOptionsBuilder<TransferDbContext>();
            var connectionString = configuration.GetConnectionString("TransferDbConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new TransferDbContext(optionsBuilder.Options);
        }
    }
}
