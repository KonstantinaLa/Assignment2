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
            var assignment = AssignmentContext.Assignments.ToList();
            return assignment;
        }

        public Assignment FindById(int? id)
        {
            var assignment = AssignmentContext.Assignments.Find(id);
            return assignment;
        }
        public void Create(Assignment assignment)
        {
            AssignmentContext.Entry(assignment).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Assignment assignment)
        {
            AssignmentContext.Entry(assignment).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Assignment assignment)
        {
            AssignmentContext.Entry(assignment).State = EntityState.Deleted;
            SaveChanges();
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