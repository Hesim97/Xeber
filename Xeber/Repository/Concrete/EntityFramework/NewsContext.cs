using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xeber.Entity;

namespace Xeber.Repository.Concrete.EntityFramework
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
           .Property(b => b.CreateDate)
           .HasDefaultValueSql("getdate()");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Xeber.Entity.NewsLang> NewsLang { get; set; }

        public DbSet<Xeber.Entity.Langs> Langs { get; set; }


    }
}

