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
        public void Create(Course course)
        {
            CourseContext.Entry(course).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Course course)
        {
            CourseContext.Entry(course).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Course course)
        {
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