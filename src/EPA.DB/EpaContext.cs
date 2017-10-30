using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EPA.Common.DTO;

namespace EPA.MSSQL.Models
{
    public class EpaContext : DbContext
    {
        static EpaContext()
        {
            Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Question, EPA.Common.DTO.Question>();
                        cfg.CreateMap<Answer, EPA.Common.DTO.Answer>();
                        cfg.CreateMap<TestDetailedInfo, TestInfo>();
                    });
        }

        public DbSet<TestDetailedInfo> Tests { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

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
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<University>().ToTable("Universities");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<ProfDirection>().ToTable("ProfDirection");
        }
    }
}