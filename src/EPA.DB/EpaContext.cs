using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EPA.MSSQL.Models
{
    public class EpaContext : DbContext
    {
        public DbSet<TestDetailedInfo> Tests { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<ProfDirection> ProfDirections { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Specialty_Subject> Specialty_Subjects { get; set; }

        public DbSet<GeneralDirection> GeneralDirections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<University>().ToTable("Universities");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<ProfDirection>().ToTable("ProfDirection");
            modelBuilder.Entity<GeneralDirection>().ToTable("GeneralDirection");
            modelBuilder.Entity<District>().ToTable("Districts");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<Specialty_Subject>().ToTable("Specialty_Subjects");
        }

        public override void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("dispose");
        }
    }
}