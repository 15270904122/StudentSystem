using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Teacher
{
    public class AddInput
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 教师级别
        /// </summary>
        public TeacherRank TeacherRank { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional Professional { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
    }
}
