using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Student
{
    public class StudentUpdateInput
    {
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

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

    }
}
