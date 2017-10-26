using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EPA.Common.DTO.ProfTest;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EPA.DB.MSSQL.Models
{
    public class EpaContext : DbContext, IDisposable
    {
        static EpaContext()
        {
            Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Question, EPA.Common.DTO.ProfTest.Quiz.Question>();
                        cfg.CreateMap<Answers, EPA.Common.DTO.ProfTest.Quiz.Answer>();
                        cfg.CreateMap<TestDetailedInfo, TestInfo>();
                    });
        }

        ~EpaContext()
        {
            this.Dispose(false);
        }

        public DbSet<TestDetailedInfo> Tests { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<ProfDirection> ProfDirections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(connectionStringConfig.GetConnectionString("EPA"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>().ToTable("Answers");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<University>().ToTable("Universities");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<ProfDirection>().ToTable("ProfDirection");
        }

        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this != null)
                {
                    this.Dispose();
                }
            }
        }
    }
}