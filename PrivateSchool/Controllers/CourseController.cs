using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using PrivateSchool.Models;
using PrivateSchool.Models.ViewModels;
using PrivateSchool.Repositories;

namespace PrivateSchool.Controllers
{
    public class CourseController : Controller
    {

        private readonly StudentRepos studentrepos;
        private readonly TrainerRepos trainerrepos;
        private readonly AssignmentRepos assignmentrepos;
        private readonly CourseRepos courserepos;

        public CourseController()
        {
            courserepos = new CourseRepos();
            trainerrepos = new TrainerRepos();
            assignmentrepos = new AssignmentRepos();
            studentrepos = new StudentRepos();
        }

        public ActionResult Index(string searchTitle, string sortOrder, int? pSize, int? page)
        {
            var courses = courserepos.GetAllCourses();

            ViewBag.currentTitle = searchTitle;
            ViewBag.currentSortOrder = sortOrder;

            ViewBag.NSP = String.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "";
           

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

            int pageSize = pSize ?? 3;
            int pageNumber = page ?? 1;

            return View(courses.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = courserepos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CourseCreateViewModel vm = new CourseCreateViewModel(studentrepos,assignmentrepos,trainerrepos);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId, Title, Stream, Type, StartDate, EndDate")] Course course, IEnumerable<int> SelectStudentIds, IEnumerable<int> SelectAssignmentIds, IEnumerable<int> SelectTrainerIds)
        {
            if (ModelState.IsValid) 
            {
                courserepos.Create(course, SelectStudentIds, SelectAssignmentIds, SelectTrainerIds);
                return RedirectToAction("Index");
            }

            CourseCreateViewModel vm = new CourseCreateViewModel(studentrepos,assignmentrepos,trainerrepos);
            return View(vm);
            
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = courserepos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            CourseEditViewModel vm = new CourseEditViewModel(course,studentrepos,assignmentrepos,trainerrepos);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId, Title, Stream, Type, StartDate, EndDate")] Course course,  IEnumerable<int> SelectStudentIds, IEnumerable<int> SelectAssignmentIds, IEnumerable<int> SelectTrainerIds)
        {
            if (ModelState.IsValid)
            {
                courserepos.Edit(course, SelectStudentIds, SelectAssignmentIds, SelectTrainerIds);
                return RedirectToAction("Index");
            }

            CourseEditViewModel vm = new CourseEditViewModel(course,studentrepos,assignmentrepos,trainerrepos);
            return View(vm);
  
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = courserepos.FindById(id);

            if (course == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {

            courserepos.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                courserepos.Dispose();
            }
            base.Dispose(disposing);
            
        }
    }
}




