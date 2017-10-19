using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EPA.DB.MSSQL.Models
{
    public class TestContext : DbContext
    {
        public DbSet<TestDetailedInfo> TestsDetailedInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDetailedInfo>().ToTable("TestsDetailedInfo");
        }
    }
}
