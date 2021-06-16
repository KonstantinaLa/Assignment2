
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
    public class CourseController : Controller
    {

        private MyDatabase db = new MyDatabase();
        private readonly CourseRepos repos;

        public CourseController()
        {
            repos = new CourseRepos();
        }
        public ActionResult Index()
        {
            return View(db.CoursesDbSet.ToList());
        }

        public ActionResult Assignment(string searchTitle, string sortOrder)
        {
            var courses = repos.GetAllCourses();

            ViewBag.currentTitle = searchTitle;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = sortOrder == "TitleAsc" ? "TitleDesc" : "TitleAsc";

            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                courses = courses.Where(a => a.Title.ToUpper().Contains(searchTitle.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "TitleDesc": courses = courses.OrderByDescending(a => a.Title).ToList(); break;
                case "TitleAsc": courses = courses.OrderBy(a => a.Title).ToList(); break;

                default: courses = courses.OrderBy(s => s.Title).ToList(); break;
            }
            return View(courses);

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = repos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId, Title, Stream, Type, Start Date, End Date")] Course course)
        {
            if (!ModelState.IsValid) return View(course);
            repos.Create(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = repos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId, Title, Stream, Type, Start Date, End Date")] Course course)
        {
            if (!ModelState.IsValid) return View(course);
            repos.Edit(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = repos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id)
        {
            var course = repos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            repos.Delete(course);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            repos.Dispose();
        }
    }
}




