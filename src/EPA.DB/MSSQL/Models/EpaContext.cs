using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EPA.DB.MSSQL.Models.Quiz;

namespace EPA.DB.MSSQL.Models
{
    public class EpaContext : DbContext
    {
        public DbSet<TestDetailedInfo> Tests { get; set; }
        public DbSet<Date> Dates { get; set; }

        public DbSet<Answers> Answers { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<TestList> TestLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>().ToTable("Answers");
            modelBuilder.Entity<Questions>().ToTable("Qestions");
            modelBuilder.Entity<TestList>().ToTable("TestLit");

            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<Date>().ToTable("Dates");
        }
    }
}