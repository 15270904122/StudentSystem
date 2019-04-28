using System.ComponentModel.DataAnnotations;

namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users : Entity<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [StringLength(50)]
        public string IdCard { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

    }

    public enum Gender
    {
        [Display(Name = "男")]
        Admin = 1,

        [Display(Name = "女")]
        Student = 2,
    }

    public enum UserType
    {
        [Display(Name = "管理员")]
        Admin = 1,

        [Display(Name = "学生")]
        Student = 2,

        [Display(Name = "教师")]
        Teacher = 3,
    }
}
