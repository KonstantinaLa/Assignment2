using FluentValidation.Attributes;
using PrivateSchool.Models.Custom_Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateSchool.Models
{
    [Validator(typeof(CourseValidator))]
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
            Assignments = new HashSet<Assignment>();
            Trainers = new HashSet<Trainer>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }

        [Display(Name = "Start Date (year/month/day)")]
        [DisplayFormat(DataFormatString = "{0:yyyy/M/d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date (year/month/day)")]
        [DisplayFormat(DataFormatString = "{0:yyyy/M/d}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }


    }
}