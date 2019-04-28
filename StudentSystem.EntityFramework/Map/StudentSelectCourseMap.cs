using StudentSystem.EntityFramework.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace StudentSystem.EntityFramework.Map
{
    public class StudentSelectCourseMap : EntityTypeConfiguration<StudentSelectCourse>
    {
        public StudentSelectCourseMap()
        {
            ToTable("StudentSelectCourse");
            HasKey(ent => ent.Id);
            Property(ent => ent.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(t => t.Students).WithMany().HasForeignKey(d => d.StudentId).WillCascadeOnDelete(false);
            HasRequired(t => t.SelectCourse).WithMany().HasForeignKey(d => d.SelectCourseId).WillCascadeOnDelete(false);
        }
    }
}
