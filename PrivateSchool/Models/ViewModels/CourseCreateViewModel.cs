using PrivateSchool.DAL;
using PrivateSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateSchool.Models.ViewModels
{
    public class CourseCreateViewModel
    {
        public AssignmentRepos assignmentRepos;
        public StudentRepos studentRepos;
        public TrainerRepos trainerRepos;

        public IEnumerable<SelectListItem> SelectStudentIds
        {
            get
            {
                return studentRepos.GetAllStudents().Select(x=>  new SelectListItem(){
                    Value = x.StudentId.ToString(),
                    Text = x.FirstName + " " + x.LastName
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
        public IEnumerable<SelectListItem> SelectTrainerIds
        {
            get
            {
                return trainerRepos.GetAllTrainers().Select(x=>  new SelectListItem(){

                    Value = x.TrainerId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                });
            }
        }
        public Course Course { get; set; }
        public CourseCreateViewModel( StudentRepos studentrepos, AssignmentRepos assignmentrepos, TrainerRepos trainerepos)
        {
            
            studentRepos = studentrepos;
            assignmentRepos = assignmentrepos;
            trainerRepos = trainerepos;
        }
    }
}