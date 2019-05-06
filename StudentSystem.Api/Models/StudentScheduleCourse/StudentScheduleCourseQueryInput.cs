using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.ScheduleCourse
{
    public class StudentScheduleCourseQueryInput : PagerInput
    {
        public Week? Week { get; set; }
        public Professional? Professional { get; set; }
    }
}
