using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PrivateSchool.DAL;
using PrivateSchool.Models;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly AssignmentRepos repos;

        public AssignmentController()
        {
            repos = new AssignmentRepos();
        }

        public ActionResult Assignment(string searchTitle, string sortOrder)
        {
            var assignments = repos.GetAllAssignments();

            ViewBag.currentTitle = searchTitle;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.LSP = sortOrder == "LastNameAsc" ? "LastNameDesc" : "LastNameAsc";

            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                assignments = assignments.Where(a => a.Title.ToUpper().Contains(searchTitle.ToUpper())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                assignments = assignments.Where(p => p.Description.ToUpper().Contains(searchTitle.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "TitleDesc": assignments = assignments.OrderByDescending(a => a.Title).ToList(); break;
                case "TitleAsc": assignments = assignments.OrderBy(a => a.Title).ToList(); break;

                case "DescriptionDesc": assignments = assignments.OrderByDescending(s => s.Description).ToList(); break;
                case "DescriptionAsc": assignments = assignments.OrderBy(s => s.Description).ToList(); break;


                default: assignments = assignments.OrderBy(s => s.Title).ToList(); break;
            }
            return View(assignments);

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = repos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentId, Title, Description, Submition Date, Oral Mark, Total Mark")] Assignment assignment)
        {
            if (!ModelState.IsValid) return View(assignment);
            repos.Create(assignment);
            return RedirectToAction("Assignment");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = repos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId, FirstName, LastName, DateOfBirth, TuitionFees")] Assignment assignment)
        {
            if (!ModelState.IsValid) return View(assignment);
            repos.Edit(assignment);
            return RedirectToAction("Assignment");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var assignment = repos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            var assignment = repos.FindById(id);

            if (assignment == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            repos.Delete(assignment);
            return RedirectToAction("Assignment");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            repos.Dispose();
        }
    }
}
