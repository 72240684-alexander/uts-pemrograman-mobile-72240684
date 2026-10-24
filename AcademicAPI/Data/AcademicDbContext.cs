
using System.Collections.Generic;
using System.Reflection.Emit;
using AcademicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Data
{
    public class AcademicDbContext : DbContext
    {
        public AcademicDbContext(DbContextOptions<AcademicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dosen> Dosen => Set<Dosen>();
        public DbSet<Mahasiswa> Mahasiswa => Set<Mahasiswa>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dosen>()
                .HasIndex(d => d.Nidn)
                .IsUnique();

            modelBuilder.Entity<Mahasiswa>()
                .HasIndex(m => m.Nim)
                .IsUnique();

            modelBuilder.Entity<Dosen>()
                .HasMany(d => d.MahasiswaWali)
                .WithOne(m => m.DosenWali)
                .HasForeignKey(m => m.DosenId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
