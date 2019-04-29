using StudentSystem.EntityFramework.Core;
using StudentSystem.Infrastructure.Extensions;

namespace StudentSystem.Api.Models.Auth
{
    public class LoginOutput
    {
        /// <summary>
        /// 身份令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        public string GenderName => Gender.DisplayName();

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

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        public string UserTypeName => UserType.DisplayName();

        /// <summary>
        /// 教师级别
        /// </summary>
        public TeacherRank? TeacherRank { get; set; }

        public string TeacherRankName => TeacherRank == null ? null : TeacherRank.DisplayName();

        /// <summary>
        /// 专业
        /// </summary>
        public Professional? Professional { get; set; }

        public string ProfessionalName => Professional == null ? null : Professional.DisplayName();

        /// <summary>
        /// 应修学分
        /// </summary>
        public int? ShouldScore { get; set; }

        /// <summary>
        /// 实修学分
        /// </summary>
        public int? RealScore { get; set; }

    }
}
