using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PokemonCategory>()
            //        .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            //modelBuilder.Entity<PokemonCategory>()
            //        .HasOne(p => p.Pokemon)
            //        .WithMany(pc => pc.PokemonCategories)
            //        .HasForeignKey(p => p.PokemonId);
            //modelBuilder.Entity<PokemonCategory>()
            //        .HasOne(p => p.Category)
            //        .WithMany(pc => pc.PokemonCategories)
            //        .HasForeignKey(c => c.CategoryId);

            //modelBuilder.Entity<PokemonOwner>()
            //        .HasKey(po => new { po.PokemonId, po.OwnerId });
            //modelBuilder.Entity<PokemonOwner>()
            //        .HasOne(p => p.Pokemon)
            //        .WithMany(pc => pc.PokemonOwners)
            //        .HasForeignKey(p => p.PokemonId);
            //modelBuilder.Entity<PokemonOwner>()
            //        .HasOne(p => p.Owner)
            //        .WithMany(pc => pc.PokemonOwners)
            //        .HasForeignKey(c => c.OwnerId);
        }
    }
}
