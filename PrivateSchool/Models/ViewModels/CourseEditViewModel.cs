using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class CourseEditViewModel
    {
        public AssignmentRepos assignmentRepos;
        public StudentRepos studentRepos;
        public TrainerRepos trainerRepos;

        public IEnumerable<SelectListItem> SelectStudentIds
        {
            get 
            {
                var studentIds = Course.Students.Select(x => x.StudentId);
                return studentRepos.GetAllStudents().Select(x => new SelectListItem()
                {
                    Value = x.StudentId.ToString(),
                    Text= x.FirstName + " " + x.LastName,
                    Selected = studentIds.Any(selected => selected == x.StudentId)
                });

            }
        }
        public IEnumerable<SelectListItem> SelectAssignmentIds
        {
            get
            {
                var assignmentIds = Course.Assignments.Select(x => x.AssignmentId);
                return assignmentRepos.GetAllAssignments().Select(x => new SelectListItem()
                {
                    Value = x.AssignmentId.ToString(),
                    Text = x.Title,
                    Selected = assignmentIds.Any(selected => selected == x.AssignmentId)
                });

            }
        }
        public IEnumerable<SelectListItem> SelectTrainerIds
        {
            get
            {
                var trainersIds = Course.Trainers.Select(x => x.TrainerId);
                return trainerRepos.GetAllTrainers().Select(x => new SelectListItem()
                {
                    Value = x.TrainerId.ToString(),
                    Text = x.FirstName + " " + x.LastName,
                    Selected = trainersIds.Any(selected => selected == x.TrainerId)
                });

            }
        }

        public Course Course { get; set; }
        public CourseEditViewModel(Course course, StudentRepos studentrepos, AssignmentRepos assignmentrepos, TrainerRepos trainerepos)
        {
            Course = course;
            studentRepos = studentrepos;
            assignmentRepos = assignmentrepos;
            trainerRepos = trainerepos;
        }
    }
}