using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using PrivateSchool.Models.ViewModels;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class TrainerController : Controller
    {
        private readonly TrainerRepos trainerrepos;
        private readonly CourseRepos courserepos;
        


        public TrainerController()
        {
            trainerrepos = new TrainerRepos();
            courserepos = new CourseRepos();
        }

        public ActionResult Index(string searchTrainer, string sortOrder)
        {
            var trainers = trainerrepos.GetAllTrainers();

            ViewBag.currentName = searchTrainer;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.LSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";

            if (!string.IsNullOrWhiteSpace(searchTrainer))
            {
                trainers = trainers.Where(p => p.FirstName.ToUpper().Contains(searchTrainer.ToUpper())).ToList();
            }


            switch (sortOrder)
            {
                case "FirstNameDesc": trainers = trainers.OrderByDescending(s => s.FirstName).ToList(); break;
                case "FirstNameAsc": trainers = trainers.OrderBy(s => s.FirstName).ToList(); break;

                case "LastNameDesc": trainers = trainers.OrderByDescending(s => s.LastName).ToList(); break;
                case "LastNameAsc": trainers = trainers.OrderBy(s => s.LastName).ToList(); break;


                default: trainers = trainers.OrderBy(s => s.FirstName).ToList(); break;
            }

            return View(trainers);

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = trainerrepos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            TrainerCreateViewModel vm = new TrainerCreateViewModel(courserepos);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId, FirstName, LastName, Subject")] Trainer trainer, IEnumerable<int> SelectCourseIds)
        {
            if (ModelState.IsValid)
            {
                trainerrepos.Create(trainer, SelectCourseIds);
                return RedirectToAction("Index");
            }

            TrainerCreateViewModel vm = new TrainerCreateViewModel(courserepos);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = trainerrepos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            TrainerEditViewModel vm = new TrainerEditViewModel(trainer,courserepos);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerId, FirstName, LastName, Subject")] Trainer trainer,IEnumerable<int> SelectCourseIds)
        {
            if (ModelState.IsValid)
            {
                trainerrepos.Edit(trainer,SelectCourseIds);

                return RedirectToAction("Index");
            }

            TrainerEditViewModel vm = new TrainerEditViewModel(trainer,courserepos);
            return View(vm);
            
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = trainerrepos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            trainerrepos.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                trainerrepos.Dispose();
            }
            base.Dispose(disposing);
            
        }

    }
}
