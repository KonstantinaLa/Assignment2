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
    public class StudentController : Controller
    {
        private readonly StudentRepos studentrepos;
        private readonly CourseRepos courserepos;
        private readonly AssignmentRepos assignmentrepos;

        public StudentController()
        {
            studentrepos = new StudentRepos();
            courserepos = new CourseRepos();
            assignmentrepos = new AssignmentRepos();
        }

        public ActionResult Index(string searchStudent, string sortOrder)
        {
            var students = studentrepos.GetAllStudents();

            ViewBag.currentName = searchStudent;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.LSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";

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

            var student = studentrepos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(student);
        }

        [HttpGet]
        public ActionResult Create()
        {
            StudentCreateViewModel vm = new StudentCreateViewModel(assignmentrepos,courserepos);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Student student, IEnumerable<int> SelectCourseIds, IEnumerable<int> SelectAssignmentIds)
        {
            if (ModelState.IsValid)
            {
                studentrepos.Create(student, SelectCourseIds, SelectAssignmentIds);

                return RedirectToAction("Index");
            }

            StudentCreateViewModel vm = new StudentCreateViewModel(assignmentrepos,courserepos);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = studentrepos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            StudentEditViewModel vm = new StudentEditViewModel(student,assignmentrepos,courserepos);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Student student, IEnumerable<int> SelectCourseIds, IEnumerable<int> SelectAssignmentIds)
        {
            if (ModelState.IsValid)
            {
                studentrepos.Edit(student, SelectCourseIds, SelectAssignmentIds);
                return RedirectToAction("Index");
            }

            StudentEditViewModel vm = new StudentEditViewModel(student,assignmentrepos,courserepos);
            return View(vm);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = studentrepos.FindById(id);

            if (student == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            studentrepos.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studentrepos.Dispose();
            }
            base.Dispose(disposing);

        }

    }
}
