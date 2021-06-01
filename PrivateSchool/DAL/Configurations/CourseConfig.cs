using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PrivateSchool.Models;

namespace PrivateSchool.DAL.Configurations
{
    public class CourseConfig : EntityTypeConfiguration<Course>
    {
        public CourseConfig()
        {
        }
    }
}