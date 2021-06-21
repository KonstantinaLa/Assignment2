using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class AssignmentCreateViewModel
    {
        public CourseRepos CourseRepos;

        public IEnumerable<SelectListItem> SelectCourseIds
        {
            get
            {
                return CourseRepos.GetAllCourses().Select(x=>  new SelectListItem(){
                    Value = x.CourseId.ToString(),
                    Text = x.Title
                });
            }
        }
        
        public Assignment Assignment{ get; set; }

        public AssignmentCreateViewModel(CourseRepos courserepos)
        {
            CourseRepos = courserepos;
        }
    }

}