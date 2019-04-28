using System;

namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 选课表
    /// </summary>
    public class SelectCourse : Entity<long>
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
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否可以开课
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 课程信息
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// 教师信息
        /// </summary>
        public virtual Teachers Teachers { get; set; }

    }
}
