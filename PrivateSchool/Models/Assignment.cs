using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrivateSchool.Models
{
    public class Assignment
    {
        public Assignment()
        {
            Courses = new HashSet<Course>();
        }
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display (Name = "Submition Date")]
        public DateTime SubDate { get; set; }

        [Display(Name ="Oral Mark")]
        public int OralMark { get; set; }

        [Display(Name = "Total Mark")]
        public int TotalMark { get; set; }

        public virtual ICollection<Course> Courses{ get; set; }
    }
}