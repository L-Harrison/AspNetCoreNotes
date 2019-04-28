using System;
using System.Collections.Generic;
using System.Text;
using EFcore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace EFcore.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            var e = this.Database;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(z => z.RoleID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(z => z.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasOne(_ => _.Orginazation)
                .WithMany(_ => _.Users)
                .HasForeignKey(m => m.OrginazationId)
                .OnDelete(deleteBehavior: DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().Property(m => m.Birth)
                .ValueGeneratedOnAddOrUpdate();


            modelBuilder.Entity<CityCompany>()
                .HasKey(x => new { x.CityID, x.CompanyID });
            modelBuilder.Entity<Mayor>()
                .HasOne(_ => _.City)
                .WithMany(_ => _.Mayor)
                .HasForeignKey(_ => _.CityID)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired();
            //modelBuilder.Entity<Word>()
            //    .HasKey(m=>m.Id)
            //    .Ha


            modelBuilder
                .Entity<Blog>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.Blog)
                //.OnDelete(DeleteBehavior.Cascade)
                //.IsRequired(true);
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
            //modelBuilder.Entity<Post>()
            //    //.HasMany(e => e.Posts)
            //    .HasOne(e => e.Blog)
            //    .WithMany(e=>e.Posts)
            //    //.OnDelete(DeleteBehavior.Cascade)
            //    //.IsRequired(true);
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .IsRequired(false);

            //.OnDelete(DeleteBehavior.ClientSetNull)
            //.IsRequired(true);
            //.OnDelete(DeleteBehavior.Restrict)
            //.IsRequired(true);
            //.OnDelete(DeleteBehavior.Cascade)
            //.IsRequired(false);
            //.OnDelete(DeleteBehavior.SetNull)
            //.IsRequired(false);
            //.OnDelete(DeleteBehavior.ClientSetNull)
            //.IsRequired(false);
            //.OnDelete(DeleteBehavior.Restrict)
            //.IsRequired(false);

            //   modelBuilder.Model.
            modelBuilder.Entity<Blog>()
                .Property(p => p.RowVersion)
                .IsRequired()
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Blog>()
                .Property(m => m.Url)
                .IsRequired()
                .IsConcurrencyToken();
            modelBuilder.Entity<Blog>()
                .Property(n => n.Bytes)
               // .HasColumnType("DATETIME")
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAddOrUpdate() ;
            base.OnModelCreating(modelBuilder);
           
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            // optionsBuilder.UseMySql("Data Source=localhost.;database=EFcore;uid=root;pwd=123456;Character Set=utf8;");
            base.OnConfiguring(optionsBuilder);

        }
    }


}
