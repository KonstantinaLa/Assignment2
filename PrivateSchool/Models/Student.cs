using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrivateSchool.Models
{
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }
      
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Tuition Fees")]
        public int TuitionFees { get; set; }

        public virtual ICollection<Course> Courses { get; set; }


    }
}