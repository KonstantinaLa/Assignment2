using System;
using System.Collections.Generic;
using System.Linq;
using PrivateSchool.Models;
using System.Data.Entity.ModelConfiguration;
using System.Web;

namespace PrivateSchool.DAL.Configurations
{
    public class TrainerConfig : EntityTypeConfiguration<Trainer> 
    { 
         public TrainerConfig()
         {
         } 
    }
}