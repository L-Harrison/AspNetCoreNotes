using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoreApi
{
    public class MyContext:DbContext
    {
        private readonly DbContextOptions _dbContext;

        public MyContext( DbContextOptions dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TODO>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
