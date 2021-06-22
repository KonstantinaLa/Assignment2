using PrivateSchool.Models;
using System.Data.Entity.ModelConfiguration;

namespace PrivateSchool.DAL.Configurations
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {


            Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(s => s.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            Property(s => s.TuitionFees)
                .IsRequired()
                .HasColumnType("int");

            //StudentAssignment
            HasMany(s => s.Assignments)
                .WithMany(a => a.Students)
                .Map(m =>
                {
                    m.ToTable("StudentsAssignments");
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("AssignmentId");

                });
        }
    }
}