﻿// <auto-generated />
using EPA.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EPA.MSSQL.Migrations
{
    [DbContext(typeof(EpaContext))]
    partial class EpaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EPA.MSSQL.Models.Answer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("QuestionID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GeneralDirectionId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GeneralDirectionId");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.GeneralDirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeneralDirection");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Logo_Universities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Logo");

                    b.HasKey("Id");

                    b.ToTable("Logo_Universities");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TestId");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DirectionId");

                    b.Property<string>("Name");

                    b.Property<int>("NumApplication");

                    b.Property<int>("NumEnrolled");

                    b.Property<int?>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.HasIndex("UniversityId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Specialty_Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SpecialtyId");

                    b.Property<int?>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Specialty_Subjects");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.TestDetailedInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApproximateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("QuestionsCount");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("TestDetailedInfoId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TestDetailedInfoId");

                    b.HasIndex("UserId");

                    b.ToTable("TestResult");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.TestScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GeneralDirectionId");

                    b.Property<int>("Score");

                    b.Property<int?>("TestResultId");

                    b.HasKey("Id");

                    b.HasIndex("GeneralDirectionId");

                    b.HasIndex("TestResultId");

                    b.ToTable("TestScore");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("DistrictId");

                    b.Property<int?>("LogoId");

                    b.Property<string>("Name");

                    b.Property<int>("Rating");

                    b.Property<string>("Site");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("DistrictId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.User_Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SpecialtyId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Specialty");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Answer", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionID");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Direction", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.GeneralDirection", "GeneralDirection")
                        .WithMany("Directions")
                        .HasForeignKey("GeneralDirectionId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Question", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.TestDetailedInfo", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Specialty", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.Direction", "Direction")
                        .WithMany("Specialties")
                        .HasForeignKey("DirectionId");

                    b.HasOne("EPA.MSSQL.Models.University", "University")
                        .WithMany("Specialties")
                        .HasForeignKey("UniversityId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.Specialty_Subject", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.Specialty", "Specialty")
                        .WithMany("SpecialtySubject")
                        .HasForeignKey("SpecialtyId");

                    b.HasOne("EPA.MSSQL.Models.Subject", "Subject")
                        .WithMany("SpecialtySubject")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.TestResult", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.TestDetailedInfo", "TestDetailedInfo")
                        .WithMany()
                        .HasForeignKey("TestDetailedInfoId");

                    b.HasOne("EPA.MSSQL.Models.User", "User")
                        .WithMany("TestResult")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.TestScore", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.GeneralDirection", "GeneralDirection")
                        .WithMany()
                        .HasForeignKey("GeneralDirectionId");

                    b.HasOne("EPA.MSSQL.Models.TestResult", "TestResult")
                        .WithMany("TestScore")
                        .HasForeignKey("TestResultId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.University", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.User", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.District", "District")
                        .WithMany("User")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.User_Specialty", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.Specialty", "Specialty")
                        .WithMany("UserSpecialt")
                        .HasForeignKey("SpecialtyId");

                    b.HasOne("EPA.MSSQL.Models.User", "User")
                        .WithMany("UserSpecialty")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EPA.MSSQL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
