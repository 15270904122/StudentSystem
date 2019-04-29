﻿using StudentSystem.EntityFramework.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace StudentSystem.EntityFramework.Map
{
    public class TeachersMap : EntityTypeConfiguration<Teachers>
    {
        public TeachersMap()
        {
            ToTable("Teachers");
            HasKey(ent => ent.Id);
            Property(ent => ent.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(t => t.Users).WithMany().HasForeignKey(d => d.UserId).WillCascadeOnDelete(false);
        }
    }
}
