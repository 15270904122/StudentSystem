using System.ComponentModel.DataAnnotations;

namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 教师表
    /// </summary>
    public class Teachers : Entity<long>
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        [StringLength(50)]
        public string TeacherNo { get; set; }

        /// <summary>
        /// 教师级别
        /// </summary>
        public TeacherRank TeacherRank { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional Professional { get; set; }

        public long UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual Users Users { get; set; }
    }
    public enum TeacherRank
    {
        [Display(Name = "教授")]
        Professional = 1,
        [Display(Name = "高级讲师")]
        Super = 2,
        [Display(Name = "讲师")]
        Middle = 3,
        [Display(Name = "助教")]
        Low = 4
    }
}
