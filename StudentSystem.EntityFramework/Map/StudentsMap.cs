using StudentSystem.EntityFramework.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace StudentSystem.EntityFramework.Map
{
    public class StudentsMap : EntityTypeConfiguration<Students>
    {
        public StudentsMap()
        {
            ToTable("Students");
            HasKey(ent => ent.Id);
            Property(ent => ent.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
