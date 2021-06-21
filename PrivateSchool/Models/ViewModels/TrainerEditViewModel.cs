using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class TrainerEditViewModel
    {
        public CourseRepos courseRepos;

        public IEnumerable<SelectListItem> SelectCourseIds
        {
            get 
            {
                var courseIds = Trainer.Courses.Select(x => x.CourseId);
                return courseRepos.GetAllCourses().Select(x => new SelectListItem()
                {
                    Value = x.CourseId.ToString(),
                    Text= x.Title,
                    Selected = courseIds.Any(selected => selected == x.CourseId)
                });

            }
        }

        public Trainer Trainer { get; set; }
        public TrainerEditViewModel(Trainer trainer, CourseRepos courserepos)
        {
            Trainer = trainer;
            courseRepos = courserepos;
        }
    }
}