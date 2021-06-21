using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class TrainerCreateViewModel
    {
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
      
        public Trainer Trainer { get; set; }

        public TrainerCreateViewModel(CourseRepos courserepos)
        {
            courseRepos = courserepos;
        }
    }
}