using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// 课程信息
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// 教师信息
        /// </summary>
        public virtual Teachers Teachers { get; set; }

    }
    public enum Week
    {
        [Display(Name = "周一")]
        One = 1,
        [Display(Name = "周二")]
        Two = 2,
        [Display(Name = "周三")]
        Three = 3,
        [Display(Name = "周四")]
        Four = 4,
        [Display(Name = "周五")]
        Five = 5,
        [Display(Name = "周六")]
        Six = 6,
        [Display(Name = "周日")]
        Seven = 7
    }
}
