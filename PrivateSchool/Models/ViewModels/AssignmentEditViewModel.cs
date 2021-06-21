using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class AssignmentEditViewModel
    {
        public CourseRepos CourseRepos;

        public IEnumerable<SelectListItem> SelectCourseIds
        {
            get 
            {
                var courseIds = Assignment.Courses.Select(x => x.CourseId);
                return CourseRepos.GetAllCourses().Select(x => new SelectListItem()
                {
                    Value = x.CourseId.ToString(),
                    Text= x.Title,
                    Selected = courseIds.Any(selected => selected == x.CourseId)
                });

            }
        }
        
        public Assignment Assignment { get; set; }
        public AssignmentEditViewModel(Assignment assignment, CourseRepos courserepos)
        {
            Assignment = assignment;
            CourseRepos = courserepos;
        }
    }
}