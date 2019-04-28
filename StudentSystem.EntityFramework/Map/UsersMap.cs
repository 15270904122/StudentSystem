using StudentSystem.EntityFramework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.EntityFramework.Map
{
    public class UsersMap : EntityTypeConfiguration<Users>
    {
        public UsersMap()
        {
            ToTable("Users");
            HasKey(ent => ent.Id);
            Property(ent => ent.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
