using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Repository.Entities
{
    public partial class Eyeglasses2024DBContext : DbContext
    {
        public Eyeglasses2024DBContext()
        {
        }

        public Eyeglasses2024DBContext(DbContextOptions<Eyeglasses2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Eyeglass> Eyeglasses { get; set; }
        public virtual DbSet<LensType> LensTypes { get; set; }
        public virtual DbSet<StoreAccount> StoreAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Eyeglass>(entity =>
            {
                entity.HasKey(e => e.EyeglassesId)
                    .HasName("PK__Eyeglass__93A83C7B963D2B7B");

                entity.Property(e => e.EyeglassesId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EyeglassesDescription).HasMaxLength(250);

                entity.Property(e => e.EyeglassesName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FrameColor).HasMaxLength(50);

                entity.Property(e => e.LensTypeId).HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.LensType)
                    .WithMany(p => p.Eyeglasses)
                    .HasForeignKey(d => d.LensTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Eyeglasse__LensT__3B75D760");
            });

            modelBuilder.Entity<LensType>(entity =>
            {
                entity.ToTable("LensType");

                entity.Property(e => e.LensTypeId).HasMaxLength(30);

                entity.Property(e => e.LensTypeDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LensTypeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StoreAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__StoreAcc__349DA586376CEEB1");

                entity.ToTable("StoreAccount");

                entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A147409FBD6F37")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.EmailAddress).HasMaxLength(60);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
