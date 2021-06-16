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
        public void Create(Student student)
        {
            StudentContext.Entry(student).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Student student)
        {
            StudentContext.Entry(student).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Student student)
        {
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