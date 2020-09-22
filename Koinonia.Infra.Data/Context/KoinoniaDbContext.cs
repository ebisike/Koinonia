using Koinonia.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Infra.Data.Context
{
    public class KoinoniaDbContext : DbContext
    {
        //protected KoinoniaDbContext _context;
        public KoinoniaDbContext(DbContextOptions<KoinoniaDbContext> opt) : base(opt)
        {

        }
        public DbSet<Comments> Comment { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<KoinoniaUsers> KoinoniaUsers { get; set; }
        public DbSet<Likes> Like { get; set; }
        public DbSet<MediaFiles> MediaFiles { get; set; }
        public DbSet<Posts> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();
            });
        }
    }
}
