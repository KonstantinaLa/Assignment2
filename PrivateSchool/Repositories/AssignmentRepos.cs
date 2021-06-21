using PrivateSchool.DAL;
using PrivateSchool.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PrivateSchool.Repositories
{
    public class AssignmentRepos
    {
        public MyDatabase AssignmentContext;

        public AssignmentRepos()
        {
            AssignmentContext = new MyDatabase();
        }
        public IEnumerable<Assignment> GetAllAssignments()
        {
            var assignment = AssignmentContext.AssignmentsDbSet.ToList();
            return assignment;
        }

        public Assignment FindById(int? id)
        {
            var assignment = AssignmentContext.AssignmentsDbSet.Find(id);
            return assignment;
        }
        public void Create(Assignment assignment,IEnumerable<int> SelectCourseIds)
        {
            Attach(assignment);
            SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = AssignmentContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        assignment.Courses.Add(course);
                    }
                }
            }

            AssignmentContext.Entry(assignment).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Assignment assignment, IEnumerable<int> SelectCourseIds)
        {
            Attach(assignment);
            assignment.Courses.Clear();
            SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = AssignmentContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        assignment.Courses.Add(course);
                    }
                }
            }
            AssignmentContext.Entry(assignment).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(int id)
        {
            var assignment = FindById(id);
            assignment.Courses.Clear();

            AssignmentContext.Entry(assignment).State = EntityState.Deleted;
            SaveChanges();
        }
        public void Attach(Assignment assignment)
        {
           
            AssignmentContext.AssignmentsDbSet.Attach(assignment);
            AssignmentContext.Entry(assignment).Collection("Courses").Load();
            
        }

        public void Dispose()
        {
            AssignmentContext.Dispose();
        }

        public void SaveChanges()
        {
            AssignmentContext.SaveChanges();
        }

    }
}