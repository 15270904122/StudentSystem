using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Student
{
    public class StudentQueryInput
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentNo { get; set; }

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
