using Koinonia.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Infra.Data.Context
{
    public class KoinoniaDbContext : DbContext
    {
        protected KoinoniaDbContext _context;
        public KoinoniaDbContext(DbContextOptions<KoinoniaDbContext> opt) : base(opt)
        {

        }
        public DbSet<News> News { get; set; }
        public virtual DbSet<Posts> Post { get; set; }
        public DbSet<Testimonies> Testimony { get; set; }
        public DbSet<Comments> Comment { get; set; }
        public DbSet<Likes> Like { get; set; }
        public DbSet<Followers> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>()
                .HasMany(x=>x.News)
        }
    }
}
