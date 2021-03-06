﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Szakdoga.DAL;

namespace Szakdoga.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181103184214_isDoneProject")]
    partial class isDoneProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Szakdoga.Model.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Szakdoga.Model.JoiningClasses.ProjectWorker", b =>
                {
                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("WorkerId");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ProjectId", "WorkerId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("ProjectWorkers");
                });

            modelBuilder.Entity("Szakdoga.Model.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<DateTimeOffset>("Deadline");

                    b.Property<Guid>("ProjectLeaderId");

                    b.Property<DateTimeOffset>("StartTime");

                    b.Property<long>("SumWorkHours");

                    b.Property<string>("Title");

                    b.Property<bool>("isDone");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Szakdoga.Model.ProjectTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDone");

                    b.Property<Guid?>("ProjectId");

                    b.Property<DateTimeOffset>("StartTime");

                    b.Property<string>("Title");

                    b.Property<long>("WorkHours");

                    b.Property<Guid?>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Szakdoga.Model.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Szakdoga.Model.TaskActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActivityId");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<string>("Description");

                    b.Property<long>("Hours");

                    b.Property<Guid>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskActivities");
                });

            modelBuilder.Entity("Szakdoga.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BossId");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("FirstSignIn")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<string>("ProfilePicture");

                    b.Property<Guid?>("RankId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.HasIndex("RankId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Szakdoga.Model.JoiningClasses.ProjectWorker", b =>
                {
                    b.HasOne("Szakdoga.Model.Project", "Project")
                        .WithMany("Workers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Szakdoga.Model.User", "Worker")
                        .WithMany("Projects")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Szakdoga.Model.ProjectTask", b =>
                {
                    b.HasOne("Szakdoga.Model.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Szakdoga.Model.User", "Worker")
                        .WithMany("Tasks")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Szakdoga.Model.TaskActivity", b =>
                {
                    b.HasOne("Szakdoga.Model.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId");

                    b.HasOne("Szakdoga.Model.ProjectTask", "Task")
                        .WithMany("TaskActivities")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Szakdoga.Model.User", b =>
                {
                    b.HasOne("Szakdoga.Model.User", "Boss")
                        .WithMany("Employees")
                        .HasForeignKey("BossId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Szakdoga.Model.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId");

                    b.OwnsOne("Szakdoga.Model.ComplexTypes.PasswordHelper", "PasswordHelper", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<byte[]>("PasswordHash");

                            b1.Property<byte[]>("PasswordSalt");

                            b1.ToTable("Users");

                            b1.HasOne("Szakdoga.Model.User")
                                .WithOne("PasswordHelper")
                                .HasForeignKey("Szakdoga.Model.ComplexTypes.PasswordHelper", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
