using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFcore.Context
{

    public class EFContextFactory : IDesignTimeDbContextFactory<EFContext>
    {
        public EFContext CreateDbContext(string[] args)
        {
            //Console.WriteLine("sa");
            //var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            // var conStr = config["ConnectionStrings:MySqlConStr"];
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseMySql("Data Source=localhost.;database=EFcore;uid=root;pwd=123456;Character Set=utf8;");

            return new EFContext(dbContextOptionsBuilder.Options);
        }
    }
}
