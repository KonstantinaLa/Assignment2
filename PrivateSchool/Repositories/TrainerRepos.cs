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
        public ICollection<Trainer> GetAllTrainers()
        {
            var trainer = TrainerContext.Trainers.ToList();
            return trainer;
        }

        public Trainer FindById(int? id)
        {
            var trainer = TrainerContext.Trainers.Find(id);
            return trainer;
        }
        public void Create(Trainer trainer)
        {
            TrainerContext.Entry(trainer).State = EntityState.Added;
            SaveChanges();
        }
        public void Edit(Trainer trainer)
        {
            TrainerContext.Entry(trainer).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Trainer trainer)
        {
            TrainerContext.Entry(trainer).State = EntityState.Deleted;
            SaveChanges();
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