using System.Collections.Generic;
using System.Linq;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using System.Data.Entity;


namespace PrivateSchool.Repositories
{
    public class TrainerRepos
    {
        public MyDatabase TrainerContext;

        public TrainerRepos()
        {
            TrainerContext = new MyDatabase();
        }
        public IEnumerable<Trainer> GetAllTrainers()
        {
            var trainer = TrainerContext.TrainersDbSet.ToList();
            return trainer;
        }

        public Trainer FindById(int? id)
        {
            var trainer = TrainerContext.TrainersDbSet.Find(id);
            return trainer;
        }
        public void Create(Trainer trainer, IEnumerable<int> SelectCourseIds)
        {
            Attach(trainer);
            TrainerContext.SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = TrainerContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        trainer.Courses.Add(course);
                    }
                }
            }

            TrainerContext.Entry(trainer).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Trainer trainer, IEnumerable<int> SelectCourseIds)
        {
            Attach(trainer);
            trainer.Courses.Clear();
            SaveChanges();

            if (!(SelectCourseIds is null))
            {
                foreach (var id in SelectCourseIds)
                {
                    Course course = TrainerContext.CoursesDbSet.Find(id);
                    if (!(course is null))
                    {
                        trainer.Courses.Add(course);
                    }
                }
            }


            TrainerContext.Entry(trainer).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(int id)
        {
            var trainer = FindById(id);
            trainer.Courses.Clear();

            TrainerContext.Entry(trainer).State = EntityState.Deleted;
            SaveChanges();
        }
        public void Attach(Trainer trainer)
        {
            TrainerContext.TrainersDbSet.Attach(trainer);
            TrainerContext.Entry(trainer).Collection("Courses").Load();
        }
        public void Dispose()
        {
            TrainerContext.Dispose();
        }

        public void SaveChanges()
        {
            TrainerContext.SaveChanges();
        }
    }
}