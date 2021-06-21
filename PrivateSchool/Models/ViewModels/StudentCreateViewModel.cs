using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class StudentCreateViewModel
    {
        public AssignmentRepos assignmentRepos;
        public CourseRepos courseRepos;

        public IEnumerable<SelectListItem> SelectCourseIds
        {
            get
            {
                return courseRepos.GetAllCourses().Select(x=>  new SelectListItem(){
                    Value = x.CourseId.ToString(),
                    Text = x.Title
                });
            }
        }
        public IEnumerable<SelectListItem> SelectAssignmentIds
        {
            get
            {
                return assignmentRepos.GetAllAssignments().Select(x=>  new SelectListItem(){

                    Value = x.AssignmentId.ToString(),
                    Text = x.Title
                });
            }
        }
        public Student Student { get; set; }

        public StudentCreateViewModel(AssignmentRepos assignmentrepos, CourseRepos courserepos)
        {
            assignmentRepos = assignmentrepos;
            courseRepos = courserepos;
        }
    }
}