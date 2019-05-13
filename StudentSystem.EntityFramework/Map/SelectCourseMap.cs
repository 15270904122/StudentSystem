using StudentSystem.EntityFramework.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace StudentSystem.EntityFramework.Map
{

    public class SelectCourseMap : EntityTypeConfiguration<SelectCourse>
    {
        public SelectCourseMap()
        {
            ToTable("SelectCourse");
            HasKey(ent => ent.Id);
            Property(ent => ent.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(t => t.Course).WithMany().HasForeignKey(d => d.CourseId).WillCascadeOnDelete(false);
            HasRequired(t => t.Teachers).WithMany().HasForeignKey(d => d.TeacherId).WillCascadeOnDelete(false);
        }
    }
}
