using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Migration(args);
            var host = CreateWebHostBuilder(args).Build();
            
            using (var scope= host.Services.CreateScope())
            {
                try
                {
                    var service = scope.ServiceProvider;
                
                }
                catch (Exception)
                {
                }
            }
             host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static async Task  Migration(string[] args)
        {
            var conStr = "";
            if (args!=null&&args.Count()>0)
            {
                conStr = args.FirstOrDefault();
            }
            else
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            conStr = configuration.GetConnectionString("MySql");
            }
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseMySql(conStr);
            Console.WriteLine("check migration");
            using ( var db=new EFcore.Context.EFContext(dbContextOptionsBuilder.Options))
            {
                db.Database.EnsureDeleted();
                if (!db.Database.EnsureCreated())
                {
                    if ((await db.Database.GetPendingMigrationsAsync()).Count()>0)
                    {
                        Console.WriteLine("Start migration");
                       await db.Database.MigrateAsync();
                       Console.WriteLine("end migration");
                    }
                    else
                    {
                        Console.WriteLine("No migration");
                    }
                }

                _ = Task.CompletedTask;
            }

            Console.WriteLine("End");
        }
    }
}
