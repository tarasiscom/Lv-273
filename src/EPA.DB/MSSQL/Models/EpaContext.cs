using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EPA.DB.MSSQL.Models.Quiz;
using System.Runtime.InteropServices;
using AutoMapper;
using EPA.Common.dto.CommonQuiz;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.Models
{
    public class EpaContext : DbContext, IDisposable
    {
        static EpaContext()
        {
            Mapper.Initialize
                (
                    cfg => {
                        cfg.CreateMap<Questions, CommonQuestions>();
                        cfg.CreateMap<Answers, CommonAnswers>();
                        cfg.CreateMap<TestDetailedInfo, CommonTestDetailedInfo>();
                    }
                );
        }
        private SafeHandle resource;

        public DbSet<TestDetailedInfo> Tests { get; set; }
        public DbSet<Date> Dates { get; set; }

        public DbSet<Answers> Answers { get; set; }
        public DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>().ToTable("Answers");
            modelBuilder.Entity<Questions>().ToTable("Questions");

            modelBuilder.Entity<TestDetailedInfo>().ToTable("Tests");
            modelBuilder.Entity<Date>().ToTable("Dates");
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EpaContext() { Dispose(false); }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (resource != null) resource.Dispose();
            }
        }
    }
}