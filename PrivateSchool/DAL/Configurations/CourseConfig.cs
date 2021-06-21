using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PrivateSchool.Models;

namespace PrivateSchool.DAL.Configurations
{
    public class CourseConfig : EntityTypeConfiguration<Course>
    {
        public CourseConfig()
        {

            Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Stream)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("date");

            Property(c => c.EndDate)
                .IsRequired()
                .HasColumnType("date");

            //TrainerCourse
            HasMany(t => t.Trainers)
                .WithMany(c => c.Courses)
                .Map(m =>
                {
                    m.ToTable("TrainersCourses");
                    m.MapLeftKey("CoursesId");
                    m.MapRightKey("TrainerId");

                });

            //StudentCourse
            HasMany(t => t.Students)
                .WithMany(c => c.Courses)
                .Map(m =>
                {
                    m.ToTable("StudentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("StudentId");

                });

            //AssignmentCourse
            HasMany(t => t.Assignments)
                .WithMany(c => c.Courses)
                .Map(m =>
                {
                    m.ToTable("AssignmentsCourses");
                    m.MapLeftKey("CourseId");
                    m.MapRightKey("AssignmentId");

                });
                
                
        }
    }
}