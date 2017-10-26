using System;
using Microsoft.EntityFrameworkCore;
using EPA.DB.MSSQL.Models.Quiz;
using AutoMapper;
using EPA.Common.DTO.ProfTest;

namespace EPA.DB.MSSQL.Models
{
    public class EpaContext : DbContext, IDisposable
    {
        static EpaContext()
        {
            Mapper.Initialize
                (
                    cfg =>
                    {
                        cfg.CreateMap<Questions, EPA.Common.DTO.ProfTest.Quiz.Question>();
                        cfg.CreateMap<Answers, EPA.Common.DTO.ProfTest.Quiz.Answer>();
                        cfg.CreateMap<TestDetailedInfo, TestInfo>();
                    }
                );
        }

        ~EpaContext()
        {
            this.Dispose(false);
        }

        public DbSet<TestDetailedInfo> Tests { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<ProfDirection> ProfDirections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>().ToTable("Answers");
            modelBuilder.Entity<Questions>().ToTable("Questions");
            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<University>().ToTable("Universities");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<ProfDirection>().ToTable("ProfDirection");
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this != null) this.Dispose();
            }
        }
    }
}