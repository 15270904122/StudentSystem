using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Course
{
    public class CourseUpdateInput
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional Professional { get; set; }

        /// <summary>
        /// 人数限制
        /// </summary>
        public int PersonLimit { get; set; }
    }
}
