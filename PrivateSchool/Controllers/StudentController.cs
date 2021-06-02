using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateSchool.Models;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepos repos;

        public StudentController()
        {
            repos = new StudentRepos();
        }

        public ActionResult Student(string searchStudent, string sortOrder)
        {
            var students = repos.GetAllStudents();

            ViewBag.currentName = searchStudent;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.LSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";

            if (!string.IsNullOrWhiteSpace(searchStudent))
            {
                students = students.Where(p => p.FirstName.ToUpper().Contains(searchStudent.ToUpper())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchStudent))
            {
                students = students.Where(p => p.FirstName.ToUpper().Contains(searchStudent.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "FirstNameDesc": students = students.OrderByDescending(s => s.FirstName).ToList(); break;
                case "FirstNameAsc": students = students.OrderBy(s => s.FirstName).ToList(); break;

                case "LastNameDesc": students = students.OrderByDescending(s => s.LastName).ToList(); break;
                case "LastNameAsc": students = students.OrderBy(s => s.LastName).ToList(); break;


                default: students = students.OrderBy(s => s.FirstName).ToList(); break;
            }
            return View(students);

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = repos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(student);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Student student)
        {
            if (!ModelState.IsValid) return View(student);
            repos.Create(student);
            return RedirectToAction("Student");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = repos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Student student)
        {
            if (!ModelState.IsValid) return View(student);
            repos.Edit(student);
            return RedirectToAction("Student");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = repos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            var student = repos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            repos.Delete(student);
            return RedirectToAction("Student");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            repos.Dispose();
        }

    }
}
