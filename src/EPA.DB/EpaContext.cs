using Microsoft.EntityFrameworkCore;
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

        public DbSet<Logo_Universities> Logo_Universities { get; set; }

        public DbSet<Direction> Directions { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Specialty_Subject> Specialty_Subjects { get; set; }

        public DbSet<GeneralDirection> GeneralDirections { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<User_Specialty> User_Specialty { get; set; }

        public DbSet<TestResult> TestResult { get; set; }

        public DbSet<TestScore> TestScore { get; set; }

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
            modelBuilder.Entity<User_Specialty>().ToTable("User_Specialty");
            modelBuilder.Entity<Logo_Universities>().ToTable("Logo_Universities");
            modelBuilder.Entity<TestResult>().ToTable("TestResult");
            modelBuilder.Entity<TestResult>().HasMany(x => x.TestScore).WithOne(p => p.TestResult).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestScore>().ToTable("TestScore");

            /*
             * Delete coment if need change name in Azure DB
             *
             modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<IdentityRole<string>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
             */
        }
    }
}