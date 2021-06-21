using System.Collections.Generic;
using System.Linq;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using System.Data.Entity;

namespace PrivateSchool.Repositories
{
    public class CourseRepos
    {

        public MyDatabase CourseContext;

        public CourseRepos()
        {
            CourseContext = new MyDatabase();
        }
        public IEnumerable<Course> GetAllCourses()
        {
            var course = CourseContext.CoursesDbSet.ToList();
            return course;
        }

        public Course FindById(int ? id)
        {
            var course = CourseContext.CoursesDbSet.Find(id);
            return course;
        }
        public void Create(Course course, IEnumerable<int> SelectStudentIds, IEnumerable<int> SelectAssignmentIds, IEnumerable<int> SelectTrainerIds)
        {
            Attach(course);
            CourseContext.SaveChanges();

            if (!(SelectStudentIds is null))
            {
                foreach (var id in SelectStudentIds)
                {
                    var student = CourseContext.StudentsDbSet.Find(id);
                    if (!(student is null))
                    {
                        course.Students.Add(student);
                    }
                }
            }

            if (!(SelectAssignmentIds is null))
            {
                foreach (var id in SelectAssignmentIds)
                {
                    var assignment = CourseContext.AssignmentsDbSet.Find(id);
                    if (!(assignment is null))
                    {
                        course.Assignments.Add(assignment);
                    }
                }
            }

            if (!(SelectTrainerIds is null))
            {
                foreach (var id in SelectTrainerIds)
                {
                    var trainer = CourseContext.TrainersDbSet.Find(id);
                    if (!(trainer is null))
                    {
                        course.Trainers.Add(trainer);
                    }
                }
            }


            CourseContext.Entry(course).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Course course, IEnumerable<int> SelectStudentIds, IEnumerable<int> SelectAssignmentIds, IEnumerable<int> SelectTrainerIds)
        {
            Attach(course);
            course.Students.Clear();
            course.Assignments.Clear();
            course.Trainers.Clear();
            CourseContext.SaveChanges();

            if (!(SelectStudentIds is null))
            {
                foreach (var id in SelectStudentIds)
                {
                    var student = CourseContext.StudentsDbSet.Find(id);
                    if (!(student is null))
                    {
                        course.Students.Add(student);
                    }
                }
            }

            if (!(SelectAssignmentIds is null))
            {
                foreach (var id in SelectAssignmentIds)
                {
                    var assignment = CourseContext.AssignmentsDbSet.Find(id);
                    if (!(assignment is null))
                    {
                        course.Assignments.Add(assignment);
                    }
                }
            }

            if (!(SelectTrainerIds is null))
            {
                foreach (var id in SelectTrainerIds)
                {
                    var trainer = CourseContext.TrainersDbSet.Find(id);
                    if (!(trainer is null))
                    {
                        course.Trainers.Add(trainer);
                    }
                }
            }
            CourseContext.Entry(course).State = EntityState.Modified;
            SaveChanges();
        }
        public void Attach(Course course)
        {
            CourseContext.CoursesDbSet.Attach(course);
            CourseContext.Entry(course).Collection("Students").Load();
            CourseContext.Entry(course).Collection("Assignments").Load();
            CourseContext.Entry(course).Collection("Trainers").Load();
        }

        public void Delete(int id)
        {
            var course = FindById(id);
            course.Students.Clear();
            course.Trainers.Clear();
            course.Assignments.Clear();

            CourseContext.Entry(course).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Dispose()
        {
            CourseContext.Dispose();
        }

        public void SaveChanges()
        {
           CourseContext.SaveChanges();
        }

    }
}