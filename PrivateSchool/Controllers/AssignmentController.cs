using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using PrivateSchool.Models.ViewModels;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class AssignmentController : Controller
    {
        
        private readonly AssignmentRepos assignmentrepos;
        private readonly CourseRepos courserepos;

        
        public AssignmentController()
        {
            courserepos = new CourseRepos();
            assignmentrepos = new AssignmentRepos();
        }

        public ActionResult Index (string searchTitle, string sortOrder)
        {
            var assignments = assignmentrepos.GetAllAssignments();

            ViewBag.CurrentTitle = searchTitle;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "TitleAsc" ? "TitleDesc" : "TitleAsc";
           

            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                assignments = assignments.Where(a => a.Title.ToUpper().Contains(searchTitle.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "TitleDesc": assignments = assignments.OrderByDescending(a => a.Title).ToList(); break;
                case "TitleAsc": assignments = assignments.OrderBy(a => a.Title).ToList(); break;


                default: assignments = assignments.OrderBy(s => s.Title).ToList(); break;
            }
            return View(assignments);

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = assignmentrepos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AssignmentCreateViewModel vm = new AssignmentCreateViewModel(courserepos);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId, Title, Description, SubDate, OralMark, TotalMark")] Assignment assignment, IEnumerable<int> SelectCourseIds)
        {
            if (ModelState.IsValid)
            {
                assignmentrepos.Create(assignment, SelectCourseIds);
                return RedirectToAction("Index");
            }

            AssignmentCreateViewModel vm = new AssignmentCreateViewModel(courserepos);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = assignmentrepos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            AssignmentEditViewModel vm = new AssignmentEditViewModel(assignment, courserepos);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentId, Title, Description, SubDate, OralMark, TotalMark")] Assignment assignment, IEnumerable<int> SelectCourseIds)
        {
            if (ModelState.IsValid)
            {
                assignmentrepos.Edit(assignment, SelectCourseIds);
                return RedirectToAction("Index");
            }

            AssignmentEditViewModel vm = new AssignmentEditViewModel(assignment, courserepos);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = assignmentrepos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            assignmentrepos.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                assignmentrepos.Dispose();
            }
            base.Dispose(disposing);
            
        }
    }
}
