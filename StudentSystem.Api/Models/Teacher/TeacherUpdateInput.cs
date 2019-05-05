using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Teacher
{
    public class TeacherUpdateInput
    {
        /// <summary>
        /// 教师级别
        /// </summary>
        public TeacherRank TeacherRank { get; set; }

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
