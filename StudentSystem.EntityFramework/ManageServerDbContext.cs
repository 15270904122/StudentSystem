using EntityFramework.DynamicFilters;
using StudentSystem.EntityFramework.Core;
using StudentSystem.EntityFramework.Filter;
using StudentSystem.EntityFramework.Map;
using System.Data.Entity;

namespace StudentSystem.EntityFramework
{
    public class ManageServerDbContext : DbContext
    {
        public ManageServerDbContext() : base("name=Default")
        {
            Database.SetInitializer<ManageServerDbContext>(null);
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Course> UserRole { get; set; }
        public DbSet<SelectCourse> Roles { get; set; }
        public DbSet<Students> Activities { get; set; }
        public DbSet<StudentSelectCourse> ActivityMessages { get; set; }
        public DbSet<Teachers> Volunteers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsersMap());
            modelBuilder.Configurations.Add(new SelectCourseMap());
            modelBuilder.Configurations.Add(new StudentSelectCourseMap());
            modelBuilder.Configurations.Add(new StudentsMap());
            modelBuilder.Configurations.Add(new TeachersMap());
            modelBuilder.Configurations.Add(new CourseMap());

            SetFilter(modelBuilder);
        }

        private void SetFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("SoftDelete", (ISoftDelete d) => d.IsDeleted, false);
        }
    }
}
