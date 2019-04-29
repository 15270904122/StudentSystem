using System.ComponentModel.DataAnnotations;

namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 学生表
    /// </summary>
    public class Students : Entity<long>
    {
        public Students()
        {
        }
        /// <summary>
        /// 学生编号
        /// </summary>
        [StringLength(50)]
        public string StudentNo { get; set; }

        /// <summary>
        /// 应修学分
        /// </summary>
        public int ShouldScore { get; set; }

        /// <summary>
        /// 实修学分
        /// </summary>
        public int RealScore { get; set; }

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

    public enum Professional
    {
        [Display(Name = "计算机专业")]
        Computer = 1,
        [Display(Name = "英文专业")]
        English = 2,
        [Display(Name = "中文专业")]
        Chinese = 3,
        [Display(Name = "数学专业")]
        Math = 4,
        [Display(Name = "医学专业")]
        Doctor = 5
    }
}
