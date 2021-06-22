using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrivateSchool.Models;
using System.Data.Entity.ModelConfiguration;

namespace PrivateSchool.DAL.Configurations
{
    public class AssignmentConfig : EntityTypeConfiguration<Assignment>
    {
        public AssignmentConfig()
        {

            Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.SubDate)
                .IsRequired()
                .HasColumnType("date");

            Property(a => a.OralMark)
                .IsRequired()
                .HasColumnType("int");

            Property(a => a.TotalMark)
                .IsRequired()
                .HasColumnType("int");

           



        }
    }
}