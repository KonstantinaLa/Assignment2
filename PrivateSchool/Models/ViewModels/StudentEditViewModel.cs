using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class StudentEditViewModel
    {
        public AssignmentRepos assignmentRepos;
        public CourseRepos courseRepos;

        public IEnumerable<SelectListItem> SelectCourseIds
        {
            get 
            {
                var courseIds = Student.Courses.Select(x => x.CourseId);
                return courseRepos.GetAllCourses().Select(x => new SelectListItem()
                {
                    Value = x.CourseId.ToString(),
                    Text= x.Title,
                    Selected = courseIds.Any(selected => selected == x.CourseId)
                });

            }
        }
        public IEnumerable<SelectListItem> SelectAssignmentIds
        {
            get
            {
                var assignmentIds = Student.Assignments.Select(x => x.AssignmentId);
                return assignmentRepos.GetAllAssignments().Select(x => new SelectListItem()
                {
                    Value = x.AssignmentId.ToString(),
                    Text = x.Title,
                    Selected = assignmentIds.Any(selected => selected == x.AssignmentId)
                });

            }
        }

        public Student Student { get; set; }
        public StudentEditViewModel(Student student,AssignmentRepos assignmentrepos, CourseRepos courserepos )
        {
            Student = student;
            assignmentRepos = assignmentrepos;
            courseRepos = courserepos;
        }
    }
}