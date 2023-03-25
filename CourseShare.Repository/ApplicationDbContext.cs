using CourseShare.Domain.DomainModels;
using CourseShare.Domain.Identity;
using CourseShare.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;

namespace CourseShare.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CourseShareApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<MyCourses> MyCourses { get; set; }
        public virtual DbSet<CourseInMyCourses> CoursesInMyCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Course>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<MyCourses>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CourseInMyCourses>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CourseInMyCourses>()
                .HasOne(z => z.CurrentCourse)
                .WithMany(z => z.CoursesInMyCourses)
                .HasForeignKey(z => z.CourseId);

            builder.Entity<CourseInMyCourses>()
                .HasOne(z => z.UserCourses)
                .WithMany(z => z.CoursesInMyCourses)
                .HasForeignKey(z => z.MyCoursesId);

            builder.Entity<MyCourses>()
                .HasOne<CourseShareApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCourses)
                .HasForeignKey<MyCourses>(z => z.OwnerId);

        }
    }
}
