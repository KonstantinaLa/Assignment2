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

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(50);



        }
    }
}