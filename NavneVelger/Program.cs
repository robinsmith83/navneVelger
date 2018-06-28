using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NavneVelger.Data;
using NavneVelger.DataContexts;

namespace NavneVelger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = BuildWebHost(args);
            MigrateDatabase(host);

            SetupUserRoles(host);

            host.Run();

        }

        public static void SetupUserRoles(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    IServiceProvider serviceProvider = services.GetRequiredService<IServiceProvider>();
                    IConfiguration configuration = services.GetRequiredService<IConfiguration>();
                    Seed.CreateRoles(serviceProvider, configuration).Wait();

                }
                catch (Exception exception)
                {
                    ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while creating roles");
                }
            }
        }

        public static void MigrateDatabase(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                BokerDb bokerContext = scope.ServiceProvider.GetRequiredService<BokerDb>();
                bokerContext.Database.Migrate();

                NavneVelgerDb navnContext = scope.ServiceProvider.GetRequiredService<NavneVelgerDb>();
                navnContext.Database.Migrate();

            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()            
                .Build();
    }
}
