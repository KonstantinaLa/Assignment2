using System.Collections.Generic;
using System.Linq;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using System.Data.Entity;

namespace PrivateSchool.Repositories
{
    public class StudentRepos
    {

        public MyDatabase StudentContext;

        public StudentRepos()
        {
            StudentContext = new MyDatabase();
        }
        public ICollection<Student> GetAllStudents()
        {
            var students = StudentContext.StudentsDbSet.ToList();
            return students;
        }

        public Student FindById(int? id)
        {
            var student = StudentContext.StudentsDbSet.Find(id);
            return student;
        }
        public void Create(Student student, IEnumerable<int> SelectCourseIds, IEnumerable<int> SelectAssignmentIds)
        {
            Attach(student);
            SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = StudentContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        student.Courses.Add(course);
                    }
                }
            }

            if (!(SelectAssignmentIds is null))
            {
                foreach (var id in SelectAssignmentIds)
                {
                    Assignment assignment = StudentContext.AssignmentsDbSet.Find(id);
                    if (!(assignment is null))
                    {
                        student.Assignments.Add(assignment);
                    }
                }
            }
            StudentContext.Entry(student).State = EntityState.Added;
           SaveChanges();
        }
        public void Edit(Student student, IEnumerable<int> SelectCourseIds, IEnumerable<int> SelectAssignmentIds)
        {
            Attach(student);
            student.Courses.Clear();
            student.Assignments.Clear();
            SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = StudentContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        student.Courses.Add(course);
                    }
                }
            }

            if (!(SelectAssignmentIds is null))
            {
                foreach (var id in SelectAssignmentIds)
                {
                    Assignment assignment = StudentContext.AssignmentsDbSet.Find(id);
                    if (!(assignment is null))
                    {
                        student.Assignments.Add(assignment);
                    }
                }
            }

            StudentContext.Entry(student).State = EntityState.Modified;
            SaveChanges();
        }

        public void Attach(Student student)
        {
            StudentContext.StudentsDbSet.Attach(student);
            StudentContext.Entry(student).Collection("Courses").Load();
            StudentContext.Entry(student).Collection("Assignments").Load();
        }

        public void Delete(int id)
        {
            var student = FindById(id);
            student.Courses.Clear();
            student.Assignments.Clear();

            StudentContext.Entry(student).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Dispose()
        {
            StudentContext.Dispose();
        }

        public void SaveChanges()
        {
            StudentContext.SaveChanges();
        }
    }
}