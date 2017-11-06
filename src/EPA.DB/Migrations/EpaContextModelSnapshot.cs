﻿// <auto-generated />
using EPA.MSSQL.Models;
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
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EPA.MSSQL.Models.Answer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Point");

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

            modelBuilder.Entity("EPA.MSSQL.Models.GeneralDirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeneralDirection");
                });

            modelBuilder.Entity("EPA.MSSQL.Models.ProfDirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DirectionId");

                    b.Property<int>("MaxPoint");

                    b.Property<int>("MinPoint");

                    b.Property<int?>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.HasIndex("TestId");

                    b.ToTable("ProfDirection");
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

            modelBuilder.Entity("EPA.MSSQL.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("District");

                    b.Property<string>("Name");

                    b.Property<string>("Site");

                    b.HasKey("Id");

                    b.ToTable("Universities");
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

            modelBuilder.Entity("EPA.MSSQL.Models.ProfDirection", b =>
                {
                    b.HasOne("EPA.MSSQL.Models.Direction", "Direction")
                        .WithMany("ProfDirections")
                        .HasForeignKey("DirectionId");

                    b.HasOne("EPA.MSSQL.Models.TestDetailedInfo", "Test")
                        .WithMany("ProfDirections")
                        .HasForeignKey("TestId");
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
#pragma warning restore 612, 618
        }
    }
}
