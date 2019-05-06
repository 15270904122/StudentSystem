using StudentSystem.EntityFramework.Core;
using StudentSystem.Infrastructure.Extensions;

namespace StudentSystem.Api.Models.ScheduleCourse
{
    public class StudentScheduleCourseQueryOutput : EntityOutput<long>
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
        /// 专业
        /// </summary>
        public string WeekName => Week.DisplayName();

        /// <summary>
        /// 课程信息
        /// </summary>
        public StudentSystem.EntityFramework.Core.Course Course { get; set; }

        /// <summary>
        /// 教师信息
        /// </summary>
        public Teachers Teachers { get; set; }
    }
}
