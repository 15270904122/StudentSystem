using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.ScheduleCourse
{
    public class ScheduleCourseUpdateInput
    {
        /// <summary>
        /// 课程Id
        /// </summary>
        public long CouresId { get; set; }

        /// <summary>
        /// 教师Id
        /// </summary>
        public long TeacherId { get; set; }

        /// <summary>
        /// 上课时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 周
        /// </summary>
        public Week Week { get; set; }

        /// <summary>
        /// 是否可以开课
        /// </summary>
        public bool IsActive { get; set; }
    }
}
