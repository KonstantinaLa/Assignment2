using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PrivateSchool.Models
{
    public class Trainer
    {
        public Trainer()
        {
            Courses = new HashSet<Course>();
        }

        public int TrainerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Subject { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }
}