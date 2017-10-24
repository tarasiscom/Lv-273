﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EPA.DB.MSSQL.Models.Quiz;

namespace EPA.DB.MSSQL.Models
{
    public class EpaContext : DbContext
    {
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
    }
}