﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EPA.MSSQL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EPA.MSSQL
{
    public class EpaContext : IdentityDbContext<User>
    {
        public EpaContext(DbContextOptions<EpaContext> op)
            : base(op)
        {
        }

        public DbSet<TestDetailedInfo> Tests { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Specialty_Subject> Specialty_Subjects { get; set; }

        public DbSet<GeneralDirection> GeneralDirections { get; set; }

        public DbSet<District> Districts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<University>().ToTable("Universities");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<GeneralDirection>().ToTable("GeneralDirection");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<Specialty_Subject>().ToTable("Specialty_Subjects");
            modelBuilder.Entity<District>().ToTable("Districts");


        }

        public override void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("dispose");
        }
    }
}