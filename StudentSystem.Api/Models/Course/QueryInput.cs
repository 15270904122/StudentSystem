using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Course
{
    public class QueryInput
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional? Professional { get; set; }
    }
}
