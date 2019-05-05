using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Teacher
{
    public class TeacherQueryInput
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional? Professional { get; set; }
    }
}
