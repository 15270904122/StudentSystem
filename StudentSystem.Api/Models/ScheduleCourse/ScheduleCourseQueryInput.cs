using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.ScheduleCourse
{
    public class ScheduleCourseQueryInput
    {
        /// <summary>
        /// 周
        /// </summary>
        public Week? Week { get; set; }

        /// <summary>
        /// 周
        /// </summary>
        public Professional? Professional { get; set; }
    }
}
