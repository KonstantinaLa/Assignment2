using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class TrainerController : Controller
    {

        private MyDatabase db = new MyDatabase();
        private readonly TrainerRepos repos;

        public TrainerController()
        {
            repos = new TrainerRepos();
        }
        public ActionResult Index()
        {
            return View(db.TrainersDbSet.ToList());
        }

        public ActionResult Student(string searchTrainer, string sortOrder)
        {
            var trainers = repos.GetAllTrainers();

            ViewBag.currentName = searchTrainer;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.LSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";

            if (!string.IsNullOrWhiteSpace(searchTrainer))
            {
                trainers = trainers.Where(p => p.FirstName.ToUpper().Contains(searchTrainer.ToUpper())).ToList();
            }
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

            var trainer = repos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerId, FirstName, LastName, Subject")] Trainer trainer)
        {
            if (!ModelState.IsValid) return View(trainer);
            repos.Create(trainer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = repos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Trainer trainer)
        {
            if (!ModelState.IsValid) return View(trainer);
            repos.Edit(trainer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var trainer = repos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            var trainer = repos.FindById(id);

            if (trainer == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            repos.Delete(trainer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            repos.Dispose();
        }

    }
}
