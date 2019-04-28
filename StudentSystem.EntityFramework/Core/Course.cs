using System.ComponentModel.DataAnnotations;

namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 课程表
    /// </summary>
    public class Course : Entity<long>
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        [StringLength(50)]
        public string CourseName { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Duration { get; set; }

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
