using Microsoft.EntityFrameworkCore;
using System;
using Szakdoga.Common;
using Szakdoga.Model;
using Szakdoga.Model.Common;
using Szakdoga.Model.ComplexTypes;
using Szakdoga.Model.JoiningClasses;
namespace Szakdoga.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(e => e.Boss)
                .WithMany(e => e.Employees)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
               .HasMany(e => e.Employees)
               .WithOne(e => e.Boss)
               .HasForeignKey(e => e.BossId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProjectWorker>()
                .HasKey(t => new { t.ProjectId, t.WorkerId });

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Workers)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.Worker)
                .HasForeignKey(e => e.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Worker)
                .HasForeignKey(e => e.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<TaskActivity> TaskActivities { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> Tasks { get; set; }

        public DbSet<ProjectWorker> ProjectWorkers{ get; set; }
    }
}
