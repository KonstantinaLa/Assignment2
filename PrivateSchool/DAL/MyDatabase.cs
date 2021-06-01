using System.Data.Entity;
using PrivateSchool.DAL.Configurations;
using PrivateSchool.Models;

namespace PrivateSchool.DAL
{
    public class MyDatabase : DbContext
    {
        public MyDatabase() : base("School")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CourseConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new AssignmentConfig());
            modelBuilder.Configurations.Add(new TrainerConfig());
        }


    }
}