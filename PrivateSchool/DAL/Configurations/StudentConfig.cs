﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PrivateSchool.Models;
using System.Data.Entity.ModelConfiguration;

namespace PrivateSchool.DAL.Configurations
{
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {


        }
    }
}