using FluentValidation.Attributes;
using PrivateSchool.Models.Custom_Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrivateSchool.Models
{
    [Validator(typeof(AssignmentValidator))]
    public class Assignment
    {
        public Assignment()
        {
            Courses = new HashSet<Course>();
        }

        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display (Name = "Submission Date (year/month/day)")]
        [DisplayFormat(DataFormatString = "{0:yyyy/M/d}", ApplyFormatInEditMode = true)]
        public DateTime SubDate { get; set; }

        [Display(Name ="Oral Mark")]
        public int OralMark { get; set; }

        [Display(Name = "Total Mark")]
        public int TotalMark { get; set; }

        public virtual ICollection<Course> Courses{ get; set; }
        public virtual ICollection<Student> Students{ get; set; }
    }
}